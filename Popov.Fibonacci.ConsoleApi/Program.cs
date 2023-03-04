using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Popov.Fibomacci.Domain;
using Popov.Fibomacci.Domain.Absttract;
using Popov.Fibonacci.ConsoleApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<IFibonacciService, FibonacciService>();
builder.Services.RegisterQueueServices(configuration);
builder.Services.RegisterApiClient(configuration);

var app = builder.Build();

await app.Services.GetRequiredService<AutoSubscriber>()
    .SubscribeAsync(AppDomain.CurrentDomain.GetAssemblies());

await app.RunCalculationsAsync();

app.Run();

