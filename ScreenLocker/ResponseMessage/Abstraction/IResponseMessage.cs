namespace ScreenLocker.ResponseMessage.Abstraction;

internal interface IResponseMessage
{
    string StartMessage();
    string UserNotAllowedMessage();
    string ScreenLockedMessage();
    string UnsupportedMessage();
    string StartApplicationMessage(string? botName);
    string ErrorMessage(int errorCode, string errorMessage);
    string IncomingRequestMessage(string message, long chatId);
}