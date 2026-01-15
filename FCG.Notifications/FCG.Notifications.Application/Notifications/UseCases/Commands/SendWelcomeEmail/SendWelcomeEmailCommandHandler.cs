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
            _logger.LogInformation($"[Sucesso] E-mail enviado | Template: Welcome | UserId: {command.UserId} | Para: {command.Email}");
            return Task.CompletedTask;
        }

    }
}