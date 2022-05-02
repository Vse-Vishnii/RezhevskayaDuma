using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Services
{
    public interface IMessageService
    {
        void Send(User sender, User receiver, Message message);
    }
}
