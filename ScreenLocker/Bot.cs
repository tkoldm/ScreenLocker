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
        _options = new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message
            }
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

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } messageText } message)
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
        switch (messageText)
        {
            case "/start":
                responseMessage = "Привет! Этот бот блокирует экран вашего компьютера!\nДля блокировки выполните /lock";
                break;
            case "/lock":
                responseMessage = "Экран заблокирован";
                Locker.LockWorkStation();
                break;
            default:
                responseMessage = "Неподдерживаемое сообщение";
                break;
        }

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: responseMessage,
            cancellationToken: cancellationToken);
    }

    private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}