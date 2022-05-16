using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RezhBot.Handlers
{
    public interface IHandler<TAction>
    {
        Task Handle(ITelegramBotClient client, TAction action, CancellationToken cancellationToken);
    }
}
