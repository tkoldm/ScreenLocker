using ScreenLocker;

var apiKey = Environment.GetEnvironmentVariable("TelebotKey")
    ?? throw new ArgumentNullException("apiKey", "Api key for telegram bot not found");

var botClient = new Bot(apiKey);

using CancellationTokenSource cts = new ();

botClient.StartReceiving(cts.Token);


var me = await botClient.GetMe();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
cts.Cancel();

