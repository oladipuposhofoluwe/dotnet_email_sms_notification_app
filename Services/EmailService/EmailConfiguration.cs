namespace NotificationApp.Services.EmailService
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }

    }
}