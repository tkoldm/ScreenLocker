using Microsoft.Extensions.Configuration;
using ScreenLocker;

var envName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

if (!string.IsNullOrEmpty(envName))
{
    builder.AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);
}

IConfiguration config = builder.Build();

var apiKey = config["TelebotKey"]
             ?? throw new ArgumentNullException("apiKey", "Api key for telegram bot not found");

var allowedUser = config["UserId"] 
                  ?? throw new ArgumentNullException("allowedUser", "User id is not found in appsettings.json");

if (!int.TryParse(allowedUser, out var allowedUserId))
{
    throw new ArgumentException("User id is incorrect");
}

var botClient = new Bot(apiKey, allowedUserId);

using CancellationTokenSource cts = new();

botClient.StartReceiving(cts.Token);


var me = await botClient.GetMe();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
cts.Cancel();