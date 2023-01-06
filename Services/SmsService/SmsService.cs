using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NotificationApp.Services.EmailService
{
    public class SmsService : ISmsService
    {
        public readonly TwilloConfig twilio;
        public SmsService(TwilloConfig twilio)
        {
            this.twilio = twilio;
        }

        public Task<MessageResource> Send(string mobileNumber, string body)
        {

        TwilioClient.Init(twilio.AccountSID, twilio.AuthToken);
        var response = MessageResource.Create(
            mobileNumber,
            from: new PhoneNumber(twilio.TwilioPhoneNumber),
            body: body
        );
            return Task.FromResult(response);
        }
    }
}