using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly SmartAlertContext _context;

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

        public IReadOnlyList<Incident> GetSimilarIncident(DateTime incDateTime, double lat, double longt, int category)
        {
            Point currentLoc = IncidentUtilityClass.CreatePoint(longt, lat);


            return  _context.Incidents.Include(s=> s.Category)
                .AsEnumerable()

                .Where(s => s.Category.Id == category
                        && s.Status == 0
                        && s.Coords.CalculateDistance(currentLoc, 'K') < 2
                        && s.DateTime  >= incDateTime.AddHours(-2) && s.DateTime <= incDateTime && 1==1
                        
                      )
                .ToList();
        }

        public async Task<IReadOnlyList<Incident>> GetNewIncidents()
        {
            return await _context.Incidents
                .Include(s => s.Details)
                .Where(s => s.Status == 0)
                .ToListAsync();
        }

        public async Task<Incident> GetIncident(int incidentId)
        {
            return await _context.Incidents
                .Include(s=> s.Category)
                .FirstOrDefaultAsync(s => s.Id == incidentId);
        }

        public void UpdateIncidentStatus(Incident incident, int status)
        {
            incident.Status = status;
            incident.StatusChangeDateTime = DateTime.UtcNow;
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
