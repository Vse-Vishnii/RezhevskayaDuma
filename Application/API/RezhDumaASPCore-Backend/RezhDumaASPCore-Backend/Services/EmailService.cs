using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RezhDumaASPCore_Backend.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private string name = "Администрация сайта";
        private string address = "Lesha37b@yandex.ru";
        private string password = "yeneibimykywveyx";

        private readonly EmailSenderOptions options;

        public EmailService(IOptions<EmailSenderOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(options.SenderName, options.HostUsername));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(options.HostAddress, options.HostPort, options.HostSecureSocketOptions);
                await client.AuthenticateAsync(options.HostUsername, options.HostPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
