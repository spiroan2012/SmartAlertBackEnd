using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task<IReadOnlyList<IncidentCategory>> GetAllTypes();
        public Task<IncidentCategory> GetTypeIdByTitle(string title);
        Task<IReadOnlyList<Incident>> GetIncidents();
    }
}
