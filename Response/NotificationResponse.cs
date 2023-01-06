using System;

namespace NotificationApp.Services.NotificationService
{
    public class NotificationResponse<T>
    {
        public T Data { get; set; }
        public Boolean Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}