using Microsoft.Extensions.Configuration;
using ScreenLocker;
using ScreenLocker.ResponseMessage;

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

var language = config["Language"] ?? "en";
var responseMessage = ResponseMessageFactory.GetResponseMessage(language);

var botClient = new Bot(settings, responseMessage);

using CancellationTokenSource cts = new();

botClient.StartReceiving(cts.Token);

var me = await botClient.GetMe();

Console.WriteLine(responseMessage.StartApplicationMessage(me.Username));
Console.ReadLine();
cts.Cancel();