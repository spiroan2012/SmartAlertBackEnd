namespace Core.Interfaces
{
    public interface ISmsService
    {
        public Task SendSms(string phoneTo, string text);
    }
}
