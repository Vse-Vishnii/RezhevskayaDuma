using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace RezhBot.Handlers
{
    public interface IHandler
    {
        Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken);
        Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken cancellationToken);
    }

    public class Handler : IHandler
    {
        private readonly TelegramBot bot;

        public Handler(TelegramBot bot)
        {
            this.bot = bot;
        }

        public Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public async Task HandleUpdate(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => ProcessMessage(client, update, cancellationToken),
                UpdateType.CallbackQuery => CallbackQuery(),
                UpdateType.InlineQuery => InlineQuery(),
                UpdateType.ChosenInlineResult => ChosenInlineResult(),
                _ => SetActionByState(client, update, cancellationToken)
            };
        }

        private async Task ProcessMessage(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message.Text switch
            {
                "Подать заявку" => SendCategories(client, update, cancellationToken),
                _ => SetActionByState(client, update, cancellationToken)
            };
        }

        private async Task SendCategories(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            
        }

        private async Task SetActionByState(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var action = bot.state switch
            {
                BotState.Started => SendStartMessage(client, update, cancellationToken)
            };
        }

        private async Task SendStartMessage(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var chat = update.Message.Chat;
            var chatId = update.Message.Chat.Id;
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"Подать заявку", "Посмотреть заявки"},
            });

            Message sentMessage = await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Привет",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
        }

        private async Task CallbackQuery()
        {
            Console.WriteLine("CallbackQuery");
        }

        private async Task InlineQuery()
        {
            Console.WriteLine("InlineQuery");
        }

        private async Task ChosenInlineResult()
        {
            Console.WriteLine("ChosenInlineResult");
        }
    }
}
