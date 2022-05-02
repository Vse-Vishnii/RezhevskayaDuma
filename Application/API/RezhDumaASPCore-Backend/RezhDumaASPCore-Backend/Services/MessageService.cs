using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IEmailService emailService;

        public MessageService(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async void Send(User sender, User receiver, Message message)
        {
            await emailService.SendEmailAsync(receiver.Email, message.Name, message.Description,
                string.Format("{0} {1} {2}", sender.Surname, sender.Firstname, sender.Patronymic));
        }
    }
}
