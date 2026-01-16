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
            var message = context.Message;

            _logger.LogInformation(
                    "Consumindo UserCreatedEvent | EventId: {EventId} | CorrelationId: {CorrelationId} | UserId: {UserId}",
                    message.EventId,
                    message.CorrelationId,
                    message.UserId
                );

            try
            {
                var command = new SendWelcomeEmailCommand(message.UserId, message.Email, message.Name);

                await _handler.Handle(command, context.CancellationToken);

                _logger.LogInformation(
                            "UserCreatedEvent processado | UserId: {UserId} | CorrelationId: {CorrelationId}",
                            message.UserId,
                            message.CorrelationId
                        );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                            ex,
                            "Falha ao processar UserCreatedEvent | UserId: {UserId} | CorrelationId: {CorrelationId} | Erro: {Message}",
                            message.UserId,
                            message.CorrelationId,
                            ex.Message
                        );
                throw;
            }

        }
    }
}