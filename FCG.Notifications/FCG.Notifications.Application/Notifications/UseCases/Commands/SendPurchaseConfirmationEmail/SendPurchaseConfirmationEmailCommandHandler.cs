using FCG.Notifications.Application.Common.Ports;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Application.Notifications.UseCases.Commands.SendPurchaseConfirmationEmail
{
    public class SendPurchaseConfirmationEmailCommandHandler : ICommandHandler<SendPurchaseConfirmationEmailCommand>
    {
        private readonly ILogger<SendPurchaseConfirmationEmailCommandHandler> _logger;

        public SendPurchaseConfirmationEmailCommandHandler(ILogger<SendPurchaseConfirmationEmailCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SendPurchaseConfirmationEmailCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                    "E-mail de confirmação de compra enviado | Template: PurchaseConfirmation | OrderId: {OrderId} | UserId: {UserId} | Status: {Status}",
                    command.OrderId,
                    command.UserId,
                    command.Status
                );

            return Task.CompletedTask;
        }
    }
}