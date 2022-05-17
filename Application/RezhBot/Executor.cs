using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RezhBot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace RezhBot
{
    public class Executor
    {
        private const string TOKEN = "5371151987:AAHIiUNXw2pgVWM9XFZ3Yny53eTVEdXBDi4";
        private TelegramBotClient client;
        private IHandler handler;

        public Executor(IHandler handler)
        {
            this.handler = handler;
            client = new TelegramBotClient(TOKEN);
        }

        public void StartBot()
        {
            var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            client.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cts.Token);

            Console.ReadLine();

            cts.Cancel();
        }

        private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            return handler.HandleError(client, exception, cancellationToken);
        }

        private Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            return handler.HandleUpdate(client, update, cancellationToken);
        }
    }
}
