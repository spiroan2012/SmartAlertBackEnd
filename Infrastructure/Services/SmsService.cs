using Core.Interfaces;
using Core.Settings;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendSms(string phoneTo, string text)
        {
            var options = _configuration.GetSection("TwilioSettings").Get<TwilioSettings>();
            TwilioClient.Init(options.Sid, options.AuthToken);

            var message = await MessageResource.CreateAsync(
                body: text,
                from: new Twilio.Types.PhoneNumber(options.FromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneTo)
            );

            Console.WriteLine(message.Sid);
            Console.WriteLine(message.Status);
        }
    }
}
