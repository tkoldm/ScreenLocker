using ScreenLocker.ResponseMessage.Abstraction;

namespace ScreenLocker.ResponseMessage.Implementations;

internal class ResponseMessageRu : IResponseMessage
{
    public string StartMessage() =>
        "Привет! Этот бот блокирует экран вашего компьютера!\nДля блокировки выполните /lock";

    public string UserNotAllowedMessage() => "Доступ запрещен";

    public string ScreenLockedMessage() => "Экран заблокирован";

    public string UnsupportedMessage() => "Неподдерживаемое сообщение";

    public string StartApplicationMessage(string? botName) =>
        string.IsNullOrEmpty(botName)
            ? "Начало обратки"
            : $"Начало обработки сообщения для @{botName}";

    public string ErrorMessage(int errorCode, string errorMessage) =>
        $"Ошибка Telegram API [{errorCode}]\n{errorMessage}";

    public string IncomingRequestMessage(string message, long chatId) =>
        $"Получено сообщение '{message}' из чата {chatId}";
}