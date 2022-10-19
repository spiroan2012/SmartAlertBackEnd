using Api.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        public IIncidentRepository _incidentRepository { get; set; }
        public ISmsRepository _smsRepository { get; set; }

        public ReportsController(IIncidentRepository incidentRepository, ISmsRepository smsRepository)
        {
            _incidentRepository = incidentRepository;
            _smsRepository = smsRepository;
        }

        [HttpGet("User")]
        public ActionResult<UserReportResponse> GetStatisticsForUser([FromQuery]string uid)
        {
            return Ok(new UserReportResponse
            {
                Success = true,
                RequestedAt = DateTime.UtcNow,
                UserReport = new UserReportDto
                {
                    TotalIncidentsReported = _incidentRepository.GetCountReportIncidentDetails(s => s.UserId == uid),
                    NumberOfIncidentsAccepted = _incidentRepository.GetCountReportIncidentDetails(s => s.UserId == uid && s.MasterIncident.Status == 2),
                    NumberOfIncidentsPending = _incidentRepository.GetCountReportIncidentDetails(s => s.UserId == uid && s.MasterIncident.Status == 0),
                    NumberOfIncidentsRejected = _incidentRepository.GetCountReportIncidentDetails(s => s.UserId == uid && s.MasterIncident.Status == 1),
                    NumberOfSmsReceived = _smsRepository.GetCountReporSmsDetails(s => s.Uid == uid),
                }
            });
        }

        [HttpGet("Admin")]
        public ActionResult<AdminReportResponse> GetStatisticsForAdmin([FromQuery] string uid)
        {
            return Ok(new AdminReportResponse
            {
                Success = true,
                RequestedAt = DateTime.UtcNow,
                AdminReport = new AdminReportDto
                {
                    NumberOfIncidentsProcessed = _incidentRepository.GetCountReportIncidents(s => s.StatusChangeUid == uid),
                    NumberOfIncidentsAccepted = _incidentRepository.GetCountReportIncidents(s => s.StatusChangeUid == uid && s.Status == 2),
                    NumberOfIncidentsPending = _incidentRepository.GetCountReportIncidents(s => s.Status == 0),
                    NumberOfIncidentsRejected = _incidentRepository.GetCountReportIncidents(s => s.StatusChangeUid == uid && s.Status == 1),
                    TotalOfIncidentsSubmited = _incidentRepository.GetCountReportIncidentsNoParams(),
                }
            });
        }
    }
}
