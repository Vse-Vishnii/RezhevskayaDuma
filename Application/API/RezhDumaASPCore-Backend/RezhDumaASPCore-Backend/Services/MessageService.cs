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

        public async void Send(User receiver, Message message, User sender= null)
        {
            var senderStr = sender == null
                ? "Режевская городская дума"
                : string.Format("{0} {1} {2}", sender.Surname, sender.Firstname, sender.Patronymic);

            await emailService.SendEmailAsync(receiver.Email, message.Name, message.Description,senderStr);
        }
    }
}
