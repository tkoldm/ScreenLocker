using Microsoft.Extensions.Configuration;
using ScreenLocker;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

IConfiguration config = builder.Build();

var settings = config.GetRequiredSection("TelegramSettings").Get<TelegramSettings>();

if (string.IsNullOrEmpty(settings?.TelebotKey) || settings.UserId == default)
{
    throw new ArgumentException("Неверное значение для параметров ApiKey или UserId. Исправьте значения в appsettings.json");
}

var botClient = new Bot(settings);

using CancellationTokenSource cts = new();

botClient.StartReceiving(cts.Token);


var me = await botClient.GetMe();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
cts.Cancel();