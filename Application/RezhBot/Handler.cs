using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RezhDumaASPCore_Backend.Model;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Message = Telegram.Bot.Types.Message;


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
        private readonly ApiService api;

        public Handler(TelegramBot bot, ApiService api)
        {
            this.bot = bot;
            this.api = api;
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
            var categories = await api.GetAll<Category>();
            var inlineButtons = new List<List<InlineKeyboardButton>>();
            for (var i = 0; i < categories.Count; i += 2)
            {
                var row = new List<InlineKeyboardButton>();
                row.Add(InlineKeyboardButton.WithCallbackData(categories[i].Name, categories[i].Name));
                if (i < categories.Count - 1)
                    row.Add(InlineKeyboardButton.WithCallbackData(categories[i + 1].Name, categories[i + 1].Name));
                inlineButtons.Add(row);
            }
            var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

            SendMessage(client, update, "Выберете категорию", cancellationToken, inlineKeyboard);
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
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"Подать заявку", "Посмотреть заявки"},
            });
            SendMessage(client, update, "Привет", cancellationToken, replyKeyboardMarkup);
        }

        private async void SendMessage(ITelegramBotClient client, Update update , string text, CancellationToken cancellationToken,
            IReplyMarkup replyKeyboard = null)
        {
            var chatId = update.Message.Chat.Id;
            Message sentMessage = await client.SendTextMessageAsync(
                chatId: chatId,
                text: text,
                replyMarkup: replyKeyboard,
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
