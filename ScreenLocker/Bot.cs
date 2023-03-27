using ScreenLocker.ResponseMessage.Abstraction;
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
    private readonly IResponseMessage _responseMessage;

    internal Bot(TelegramSettings telegramSettings, IResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
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
        Console.WriteLine(_responseMessage.IncomingRequestMessage(messageText, chatId));

        if (chatId != _allowedUser)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: _responseMessage.UserNotAllowedMessage(),
                cancellationToken: cancellationToken);
            return;
        }

        string responseMessage;
        switch (messageText)
        {
            case "/start":
            {
                responseMessage = _responseMessage.StartMessage();
                break;
            }
            case "/lock":
            {
                responseMessage = _responseMessage.ScreenLockedMessage();
                Locker.LockWorkStation();
                break;
            }
            default:
            {
                responseMessage = _responseMessage.UnsupportedMessage();
                break;
            }
        }

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: responseMessage,
            cancellationToken: cancellationToken);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => _responseMessage.ErrorMessage(apiRequestException.ErrorCode, apiRequestException.Message),
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}