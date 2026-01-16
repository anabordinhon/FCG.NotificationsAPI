using FCG.Notifications.Application.Common.Ports;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Application.Notifications.UseCases.Commands.SendWelcomeEmail
{
    public class SendWelcomeEmailCommandHandler : ICommandHandler<SendWelcomeEmailCommand>
    {
        private readonly ILogger<SendWelcomeEmailCommandHandler> _logger;

        public SendWelcomeEmailCommandHandler(ILogger<SendWelcomeEmailCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SendWelcomeEmailCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                    "E-mail de boas-vindas enviado | Template: {Template} | UserId: {UserId} | Destinatário: {Email}",
                    "Welcome",
                    command.UserId,
                    command.Email
                );

            return Task.CompletedTask;
        }

    }
}