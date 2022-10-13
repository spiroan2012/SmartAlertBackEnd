using Core.Entities;

namespace Core.Interfaces
{
    public interface ISmsRepository
    {
        public void AddSmsMaster(SmsMaster smsMaster);
        public void AddSmsDetail(SmsDetail smsDetail);
        public int GetCountReporSmsDetails(Func<SmsDetail, bool> condition);
        public Task<bool> Complete();
    }
}
