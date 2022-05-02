using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RezhDumaASPCore_Backend.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, string sender);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSenderOptions options;

        public EmailService(IOptions<EmailSenderOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message, string sender)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(sender, options.HostUsername));
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
