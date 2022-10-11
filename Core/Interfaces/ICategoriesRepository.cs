using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task<IReadOnlyList<IncidentCategory>> GetAllTypes();
        public Task<IncidentCategory> GetTypeIdByTitle(string title);
        Task<IReadOnlyList<Incident>> GetIncidents();
    }
}
