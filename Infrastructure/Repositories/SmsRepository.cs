using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

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

        public int GetCountReporSmsDetails(Func<SmsDetail, bool> condition) =>
                    _context.SmsDetails
                    .Count(condition);

    }
}
