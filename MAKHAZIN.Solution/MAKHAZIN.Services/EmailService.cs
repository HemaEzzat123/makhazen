using MailKit.Net.Smtp;
using MAKHAZIN.Core.Services.Contract;
using MAKHAZIN.Services.Configurations;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MAKHAZIN.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _options;

        public EmailService(IOptions<EmailSettings> options)
        {
            _options = options.Value;
        }
        public void SendEmail(string to, string subject, string body)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = subject,
            };

            mail.To.Add(MailboxAddress.Parse(to));
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            var builder = new BodyBuilder();

            builder.TextBody = body;

            mail.Body = builder.ToMessageBody();

            // Establish SMTP client and send the email

            using var smpt = new SmtpClient();

            smpt.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);

            smpt.Authenticate(_options.Email, _options.Password);

            smpt.Send(mail);

            smpt.Disconnect(true);
        }
    }
}
