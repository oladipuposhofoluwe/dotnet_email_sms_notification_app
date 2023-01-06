using Twilio.Rest.Api.V2010.Account;

namespace NotificationApp.Services.EmailService
{
    public interface ISmsService
    {
    Task<MessageResource> Send(string mobileNumber, string body);

    }
}