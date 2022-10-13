using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface IIncidentRepository
    {
        public void AddMaster(Incident incident);
        public void AddDetail(IncidentDetail incident);
        public int GetCountOfLaterIncidents(DateTime incDateTime, double lat, double longt, int category);
        public IReadOnlyList<Incident> GetSimilarIncident(DateTime incDateTime, double lat, double longt, int category);
        public Task<IReadOnlyList<Incident>> GetNewIncidents();
        public Task<Incident> GetIncident(int incidentId);
        public void UpdateIncidentStatus(Incident incident, int status, string uid);
        public void UpdateIncidentDetailsStatus(IncidentDetail[] Details, string uid);
        public int GetCountReportIncidents(Func<Incident, bool> condition);
        public int GetCountReportIncidentsNoParams();
        public int GetCountReportIncidentDetails(Func<IncidentDetail, bool> condition);
        public Task<bool> Complete();
    }
}
