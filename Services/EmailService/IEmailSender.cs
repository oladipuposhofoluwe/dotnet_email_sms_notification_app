namespace NotificationApp.Services.EmailService
{
    public interface IEmailSender
    {
        Task SendMailNotification(MailRequest request);
    }
}