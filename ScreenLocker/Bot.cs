using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ScreenLocker;

internal class Bot
{
    private readonly ITelegramBotClient _botClient;
    private readonly int _allowedUser;
    private readonly ReceiverOptions _options;

    internal Bot(TelegramSettings telegramSettings)
    {
        _botClient = new TelegramBotClient(telegramSettings.TelebotKey);
        _options = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };
        _allowedUser = telegramSettings.UserId;
    }

    internal void StartReceiving(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: _options,
            cancellationToken:
            cancellationToken);
    }

    internal async Task<User> GetMe() => await _botClient.GetMeAsync();

    async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;
        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        if (chatId != _allowedUser)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Доступ запрещен",
                cancellationToken: cancellationToken);
            return;
        }


        string responseMessage;
        if (messageText == "/lock")
        {
            responseMessage = "Экран заблокирован";
            Locker.LockWorkStation();
        }
        else
        {
            responseMessage = "Неподдерживаемое сообщение";
        }

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: responseMessage,
            cancellationToken: cancellationToken);
    }

    Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
}