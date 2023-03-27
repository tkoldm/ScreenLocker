namespace ScreenLocker;

internal class TelegramSettings
{
    internal string TelebotKey { get; }
    internal int UserId { get; }
    
    public TelegramSettings(string telebotKey, int userId)
    {
        TelebotKey = telebotKey;
        UserId = userId;
    }
}