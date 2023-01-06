using NotificationApp.Dto;
using NotificationApp.Notification;
using NotificationApp.Response;
using NotificationApp.Services.EmailService;

namespace NotificationApp.Services.NotificationService
{
    public class SendNotifications : ISendNotification
    {
        private readonly IEmailSender emailSender;
        private readonly ISmsService smsService;

        public SendNotifications(IEmailSender emailSender, ISmsService smsService)
        {
            this.emailSender = emailSender;
            this.smsService = smsService;
        }

        public async Task<NotificationResponse<string>> SendNotification(SendRequestDto request)
        {
            var response = new NotificationResponse<string>();
            try{

                if(request.notificationType == NotificationType.Email){
                    var message = new MailRequest(request.ToEmail, request.EmailSubject, request.BodyContent, request.ToUserName);
                    await emailSender.SendMailNotification(message);
                    ApiResponse.getResponse(response, "Email sucessfully sent to {0}", request); 
                }else{
                    var sms = await smsService.Send(request.ToNumber, request.BodyContent);
                    ApiResponse.getResponse(response, request.ToNumber, "SMS sucessfully sent to {0}"); 
                }
                return response;

        } catch(System.Exception ex){
            Console.WriteLine("EXCEPTION " + ex.Message);
            return response;
        }
        }
    }
}