using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly SmartAlertContext? _context;

        public IncidentRepository(SmartAlertContext context)
        {
            _context = context;
        }
        public void AddDetail(IncidentDetail incidentDet)
        {
            _context.IncidentDetails.Add(incidentDet);
        }

        public void AddMaster(Incident incident)
        {
            _context.Incidents.Add(incident);
        }

        public int GetCountOfLaterIncidents(DateTime incDateTime, double lat, double longt, int category)
        {
            Point currentLoc = IncidentUtilityClass.CreatePoint(longt, lat);


            return _context.Incidents.Include(s => s.Category)
                .Include(s => s.Details)
                .AsEnumerable()

                .Where(s => s.Category.Id == category
                        && s.Status == 0
                        && s.Coords.CalculateDistance(currentLoc, 'K') < 2
                        && s.DateTime > incDateTime

                      )
                .Count();
        }

        public IReadOnlyList<Incident> GetSimilarIncident(DateTime incDateTime, double lat, double longt, int category)
        {
            Point currentLoc = IncidentUtilityClass.CreatePoint(longt, lat);


            return _context.Incidents.Include(s => s.Category)
                .Include(s => s.Details)
                .AsEnumerable()

                .Where(s => s.Category.Id == category
                        && s.Status == 0
                        && s.Coords.CalculateDistance(currentLoc, 'K') < 2
                        && s.DateTime >= incDateTime.AddHours(-2) && s.DateTime <= incDateTime && 1 == 1

                      )
                .ToList();
        }

        public async Task<IReadOnlyList<Incident>> GetNewIncidents()
        {
            return await _context.Incidents
                .Include(s => s.Details)
                .Include(s => s.Category)
                .Where(s => s.Status == 0)
                .ToListAsync();
        }

        public async Task<Incident> GetIncident(int incidentId)
        {
            return await _context.Incidents
                .Include(s => s.Category)
                .Include(s => s.Details)
                .FirstOrDefaultAsync(s => s.Id == incidentId);
        }

        public void UpdateIncidentStatus(Incident incident, int status, string uid)
        {
            incident.Status = status;
            incident.StatusChangeDateTime = DateTime.UtcNow;
            incident.StatusChangeUid = uid;
        }

        public void UpdateIncidentDetailsStatus(IncidentDetail[] Details, string uid)
        {
            for(int i = 0;i < Details.Length; i++)
            {
                Details[i].StatusChangeUid = uid;
                Details[i].StatusChangeDateTime = DateTime.UtcNow;
            }
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetNumberOfIncidentsReportedForUser(string uid)
        {
            return _context.IncidentDetails
                .Count(s => s.UserId == uid);
        }
    }
}
