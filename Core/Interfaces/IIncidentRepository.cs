using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IIncidentRepository
    {
        public void AddMaster(Incident incident);
        public void AddDetail(IncidentDetail incident);
        public IReadOnlyList<Incident> GetSimilarIncident(DateTime incDateTime, double lat, double longt, int category);
        public Task<IReadOnlyList<Incident>> GetNewIncidents();
        public Task<Incident> GetIncident(int incidentId);
        public void UpdateIncidentStatus(Incident incident, int status, string uid);
        public Task<bool> Complete();
    }
}
