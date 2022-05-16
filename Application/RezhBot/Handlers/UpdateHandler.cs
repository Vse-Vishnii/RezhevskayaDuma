using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace RezhBot.Handlers
{
    internal class UpdateHandler : IHandler<Update>
    {
        public async Task Handle(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            //ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            //{
            //    new KeyboardButton[] { "Help me", "Call me ☎️" },
            //})
            //{
            //    ResizeKeyboard = true
            //};

            //Message sentMessage = await client.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "Choose a response",
            //    replyMarkup: replyKeyboardMarkup,
            //    cancellationToken: cancellationToken);
        }
    }
}
