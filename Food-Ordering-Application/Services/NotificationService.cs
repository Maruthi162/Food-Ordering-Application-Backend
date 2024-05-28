using System.Net.Mail;

namespace Food_Ordering_Application.Services
{
    public class NotificationService
    {
        private readonly SmtpClient _smtpClient;
        public NotificationService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        public async Task SendEmail(string toEmail, string subject, string body)
        {

            var mailMessage = new MailMessage
            {
                From = new MailAddress("nmaruthi162@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
