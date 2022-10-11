using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NetTopologySuite.Geometries;
using SmartAlertApi.Dtos;

namespace SmartAlertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        public IIncidentRepository _incidentRepository { get; set; }
        public ICategoriesRepository _categoryRepository { get; set; }
        public ISmsRepository _smsRepository { get; set; }
        public IFirebaseService _firebaseService { get; set; }
        public ISmsService _smsService { get; set; }
        public IMapper _mapper { get; set; }


        public IncidentsController(IIncidentRepository incidentRepository,
            ICategoriesRepository categoryRepository,
            IFirebaseService firebaseService,
            ISmsService smsService,
            ISmsRepository smsRepository,
            IMapper mapper)
        {
            _incidentRepository = incidentRepository;
            _categoryRepository = categoryRepository;
            _firebaseService = firebaseService;
            _smsService = smsService;
            _mapper = mapper;
            _smsRepository = smsRepository;
        }


        [HttpPost("AddIncident")]
        public async Task<ActionResult<AddIncidentResponse>> Add(AddIncidentDto incidentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AddIncidentResponse
                {
                    Success = false,
                    RequestedAt = DateAndTime.Now,
                    Message = ModelState.ToString()
                });
            }

            IncidentCategory category = await _categoryRepository
                .GetTypeIdByTitle(incidentDto.Category);

            if (category == null)
            {
                return BadRequest(new AddIncidentResponse
                {
                    Success = false,
                    RequestedAt = DateAndTime.Now,
                    Message = $"Coud not find category {incidentDto.Category}"
                });
            }

            if (_incidentRepository.GetCountOfLaterIncidents(incidentDto.DateTime, incidentDto.Latitude, incidentDto.Longitude, category.Id) != 0)
            {
                return BadRequest(new AddIncidentResponse
                {
                    Success = false,
                    RequestedAt = DateAndTime.Now,
                    Message = $"Cannot create incident which is from the past"
                });
            }

            var similarIncident = _incidentRepository
                .GetSimilarIncident(incidentDto.DateTime, incidentDto.Latitude, incidentDto.Longitude, category.Id);
            if (similarIncident.Count > 1)
            {
                return BadRequest(new AddIncidentResponse
                {
                    Success = false,
                    RequestedAt = DateAndTime.Now,
                    Message = "There are more than one similar incidents"
                });
            }


            IncidentDetail det = new IncidentDetail
            {
                CreationDateTime = DateTime.UtcNow,
                Title = incidentDto.Title,
                DateTime = incidentDto.DateTime,
                Description = incidentDto.Description,
                Category = category,
                UserId = incidentDto.Uid,
                UserEmail = incidentDto.Email,
                ImageUrl = incidentDto.Image,
                Address = incidentDto.HumanAddress,
                Coords = IncidentUtilityClass.CreatePoint(incidentDto.Longitude, incidentDto.Latitude)
            };
            if (similarIncident.Count == 0)
            {
                Incident inc = new Incident
                {
                    CreationDateTime = DateTime.UtcNow,
                    DateTime = incidentDto.DateTime,
                    Category = category,
                    Status = 0,
                    Coords = IncidentUtilityClass.CreatePoint(incidentDto.Longitude, incidentDto.Latitude)
                };
                _incidentRepository.AddMaster(inc);

                det.MasterIncident = inc;
            }
            else
            {
                if (similarIncident[0].Details.Any(s => s.UserId == incidentDto.Uid))
                {
                    return BadRequest(new AddIncidentResponse
                    {
                        Success = false,
                        RequestedAt = DateAndTime.Now,
                        Message = "User has already submitted the incident"
                    });
                }

                det.MasterIncident = similarIncident[0];
            }

            _incidentRepository.AddDetail(det);

            if (await _incidentRepository.Complete())
                return Ok(new AddIncidentResponse
                {
                    Success = true,
                    RequestedAt = DateAndTime.Now,
                    Message = $"Incident {incidentDto.Title} added succesfully"
                });
            // return Ok("Successfully added incident");

            return new AddIncidentResponse
            {
                Success = false,
                RequestedAt = DateAndTime.Now,
                Message = $"Failed to add incident {incidentDto.Title}"
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetIncidents()
        {
            var incidents = await _incidentRepository.GetNewIncidents();
            List<IncidentDto> incidentsDtoList = _mapper.Map<List<IncidentDto>>(incidents);
            return Ok(new IncidentResponse
            {
                Success = true,
                RequestedAt = DateTime.UtcNow,
                Results = incidentsDtoList.Count(),
                Data = new IncidentData
                {
                    Incidents = incidentsDtoList.ToArray()
                }
            });
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<FirebaseUser>>> GetUsers()
        {
            var users = await _firebaseService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("CalculateDistance")]
        public ActionResult<double> GetDistance(DistanceDto distance)
        {
            Point point1 = IncidentUtilityClass.CreatePoint(distance.Lon1, distance.Lat1);
            return point1.CalculateDistance(new Point(distance.Lon2, distance.Lat2), 'K');
        }


        [HttpPatch]
        public async Task<ActionResult<AddIncidentResponse>> UpdateStatus(UpdateIncidentDto updateDto)
        {
            Incident incident = await _incidentRepository.GetIncident(updateDto.IncidentId);
            if (incident == null)
            {
                return BadRequest(new AddIncidentResponse
                {
                    Success = false,
                    RequestedAt = DateTime.Now,
                    Message = $"No master incident was found with id {updateDto.IncidentId}"
                });
            }
            else if (incident.Status != 0)
            {
                return BadRequest(new AddIncidentResponse
                {
                    Success = false,
                    RequestedAt = DateTime.Now,
                    Message = $"Incident with id {updateDto.IncidentId} has already been proccessed"
                });
            }
            var users = await _firebaseService.GetAllUsers();
            Point masterPoint = IncidentUtilityClass.CreatePoint(incident.Coords.X, incident.Coords.Y);
            if (updateDto.Status == 2)
            {
                SmsMaster master = new SmsMaster
                {
                    Incident = incident,
                    SmsText = incident.Category.SmsText
                };

                _smsRepository.AddSmsMaster(master);
                foreach (var user in users)
                {
                    Point userPoint = IncidentUtilityClass.CreatePoint(user.Longitude, user.Latitude);
                    if (masterPoint.CalculateDistance(userPoint, 'K') <= 2)
                    {
                        //await _smsService.SendSms($"+30{user.MobilePhone}", incident.Category.SmsText);

                        SmsDetail detail = new SmsDetail
                        {
                            MobilePhone = user.MobilePhone,
                            SmsMaster = master
                        };

                        _smsRepository.AddSmsDetail(detail);
                    }
                }
            }

            _incidentRepository.UpdateIncidentStatus(incident, updateDto.Status, updateDto.Uid);

            if (await _incidentRepository.Complete()) return Ok(new AddIncidentResponse
            {
                Success = true,
                RequestedAt = DateTime.Now,
                Message = "Incident status changed successfully"
            });

            return BadRequest(new AddIncidentResponse
            {
                Success = false,
                RequestedAt = DateTime.Now,
                Message = "Failed to update incident status"
            });
        }
    }
}