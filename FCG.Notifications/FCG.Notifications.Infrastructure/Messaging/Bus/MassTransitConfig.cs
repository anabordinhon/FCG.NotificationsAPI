using FCG.Notifications.Domain.Shared.Events;
using FCG.Notifications.Infrastructure.Consumers;
using FCG.Users.Application.Users.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Notifications.Infrastructure.Messaging.Bus;

public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransitConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitHost = configuration["RabbitMq:Host"] ?? "localhost";
        var rabbitUser = configuration["RabbitMq:Username"] ?? "guest";
        var rabbitPass = configuration["RabbitMq:Password"] ?? "guest";

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            //x.AddConsumer<PaymentProcessedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitHost, "/", h =>
                {
                    h.Username(rabbitUser);
                    h.Password(rabbitPass);
                });
                cfg.Message<UserCreatedEvent>(m => m.SetEntityName("user-created-exchange"));

                cfg.ReceiveEndpoint("notifications-user-created-queue", e =>
                {
                    // Define que esta fila deve ouvir a exchange fixa
                    e.Bind("user-created-exchange");

                    // Conecta o seu Consumer à esta fila
                    e.ConfigureConsumer<UserCreatedConsumer>(context);
                });

            });

        });

        return services;
    }
}