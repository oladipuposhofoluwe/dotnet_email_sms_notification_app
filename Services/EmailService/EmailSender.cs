using MimeKit;


namespace NotificationApp.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfiguration;
        public EmailSender(EmailConfiguration _emailconf)
        {
            emailConfiguration= _emailconf;
        }
        async Task IEmailSender.SendMailNotification(MailRequest request)
        {
            var emailMessage = CreateEmailMessage(request);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MailRequest request)
        {
            string fileDir = "./EmailTemplate.html";
            StreamReader str = new StreamReader(fileDir);
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText.Replace("[username]", request.UserName).Replace("[title]", request.Subject).Replace("[body]", request.Body);

            var mineMessage = new MimeMessage();

            mineMessage.From.Add(new  MailboxAddress(emailConfiguration.DisplayName, emailConfiguration.From));
            mineMessage.To.Add(new MailboxAddress(null, request.To));
            mineMessage.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = MailText;
            mineMessage.Body = bodyBuilder.ToMessageBody();
            return mineMessage;
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using(var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try {
                    await client.ConnectAsync(emailConfiguration.Host, emailConfiguration.Port, useSsl:true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(emailConfiguration.From, emailConfiguration.Password);
                    await client.SendAsync(emailMessage);
                }catch(System.Exception ex){
                    System.Console.WriteLine(ex.Message);
                    throw;
                }finally{
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

    }
    
}