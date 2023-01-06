using NotificationApp.Notification;

namespace NotificationApp.Dto
{
    public class SendRequestDto
    {
        public string ToNumber { get; set; }
        public string ToEmail { get; set; }
        public string EmailSubject { get; set; }
        public string BodyContent { get; set; }
        public string ToUserName { get; set; }
        public NotificationType notificationType { get; set; }
    }
}