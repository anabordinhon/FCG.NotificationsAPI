using FCG.Notifications.Application.Common.Ports;
using FCG.Notifications.Application.Notifications.UseCases.Commands.SendWelcomeEmail;
using FCG.Users.Application.Users.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Infrastructure.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly ICommandHandler<SendWelcomeEmailCommand> _handler;
        private readonly ILogger<UserCreatedConsumer> _logger;

        public UserCreatedConsumer(
            ICommandHandler<SendWelcomeEmailCommand> handler,
            ILogger<UserCreatedConsumer> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            _logger.LogInformation("[Infra] ENTROU no UserCreatedConsumer");

            var message = context.Message;

            // Aqui montamos o Comando da Aplicação
            var command = new SendWelcomeEmailCommand(message.UserId, message.Email, message.Name);

            // Chamamos a camada de aplicação para executar a regra
            await _handler.Handle(command, context.CancellationToken);

            _logger.LogInformation($"[Infra] Evento processado e comando enviado para Application.");
        }
    }
}