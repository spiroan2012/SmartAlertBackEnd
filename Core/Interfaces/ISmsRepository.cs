using Core.Entities;

namespace Core.Interfaces
{
    public interface ISmsRepository
    {
        public void AddSmsMaster(SmsMaster smsMaster);
        public void AddSmsDetail(SmsDetail smsDetail);
        public Task<bool> Complete();
    }
}
