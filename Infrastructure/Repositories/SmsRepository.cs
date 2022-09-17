using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SmsRepository : ISmsRepository
    {
        private readonly SmartAlertContext _context;

        public SmsRepository(SmartAlertContext context)
        {
            _context = context;
        }

        public void AddSmsDetail(SmsDetail smsDetail)
        {
            _context.SmsDetails.Add(smsDetail);
        }

        public void AddSmsMaster(SmsMaster smsMaster)
        {
            _context.SmsMasters.Add(smsMaster);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
