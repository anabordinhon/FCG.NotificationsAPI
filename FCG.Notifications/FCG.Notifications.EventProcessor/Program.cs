using FCG.Notifications.Application.Common.Ports;
using FCG.Notifications.Application.Notifications.UseCases.Commands.SendWelcomeEmail;
using FCG.Notifications.Infrastructure.Messaging.Bus;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitConfiguration(builder.Configuration);
builder.Services.AddScoped<ICommandHandler<SendWelcomeEmailCommand>, SendWelcomeEmailCommandHandler>();
builder.Logging.ClearProviders();             
builder.Logging.AddConsole();                 
builder.Logging.SetMinimumLevel(LogLevel.Debug);


var host = builder.Build();
await host.RunAsync();
