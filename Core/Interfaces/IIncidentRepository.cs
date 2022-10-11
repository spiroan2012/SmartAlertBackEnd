﻿using Core.Entities;

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
        public Task<bool> Complete();
    }
}
