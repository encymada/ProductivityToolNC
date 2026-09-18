using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ProductivityToolAppService
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(
            string recipientEmail,
            string taskName,
            string taskDescription)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _configuration["EmailSettings:FromName"],
                _configuration["EmailSettings:FromEmail"]
            ));

            message.To.Add(new MailboxAddress(
                "Productivity Tool User",
                recipientEmail
            ));

            message.Subject =
                "Productivity Tool - New Task Added";

            message.Body = new TextPart("plain")
            {
                Text =
                    "A new task has been added to your Productivity Tool.\n\n" +
                    $"Task Name: {taskName}\n" +
                    $"Description: {taskDescription}\n\n" +
                    "Status: PENDING"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(
                    _configuration["EmailSettings:SmtpHost"],
                    int.Parse(
                        _configuration["EmailSettings:SmtpPort"]
                    ),
                    SecureSocketOptions.StartTls
                );

                client.Authenticate(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                );

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}