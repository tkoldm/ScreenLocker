using ScreenLocker.ResponseMessage.Abstraction;
using ScreenLocker.ResponseMessage.Implementations;

namespace ScreenLocker.ResponseMessage;

internal static class ResponseMessageFactory
{
    internal static IResponseMessage GetResponseMessage(string language)
    {
        switch (language)
        {
            case "ru":
            {
                return new ResponseMessageRu();
            }
            case "en":
            default:
            {
                return new ResponseMessageEn();
            }
        }
    }
}