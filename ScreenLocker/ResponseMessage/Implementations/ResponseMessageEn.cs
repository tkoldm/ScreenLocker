using ScreenLocker.ResponseMessage.Abstraction;

namespace ScreenLocker.ResponseMessage.Implementations;

internal class ResponseMessageEn : IResponseMessage
{
    public string StartMessage() =>
        "Hello! This bot is locking your computer screen!\nTo lock, type /lock";

    public string UserNotAllowedMessage() => "Access is denied";

    public string ScreenLockedMessage() => "Screen locked";

    public string UnsupportedMessage() => "Unsupported message";
    public string StartApplicationMessage(string? botName) => 
        string.IsNullOrEmpty(botName) 
            ? "Start listening"
            : $"Start listening for @{botName}";
    
    public string ErrorMessage(int errorCode, string errorMessage) => 
        $"Telegram API Error:\n[{errorCode}]\n{errorMessage}";

    public string IncomingRequestMessage(string message, long chatId) =>
        $"Received a '{message}' message in chat {chatId}.";
}