using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly SmartAlertContext _context;

        public CategoriesRepository(SmartAlertContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<IncidentCategory>> GetAllTypes() => await _context.Categories.ToListAsync();

        public async Task<IncidentCategory> GetTypeIdByTitle(string title) => await _context.Categories
                .Where(t => t.Type.ToLower() == title.ToLower())
                .FirstOrDefaultAsync();

        public async Task<IReadOnlyList<Incident>> GetIncidents()
        {
            return await _context.Incidents
                .Include(s => s.Details)
                .Where(i => i.Status == 0)
                .ToListAsync();
        }
    }
}
