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
            _logger.LogInformation("------------------------------------------------");
            _logger.LogInformation("[NOTIFICAÇÃO] Envio de e-mail de CONFIRMAÇÃO DE COMPRA");
            _logger.LogInformation("Pedido: {OrderId} | Usuário: {UserId} | Status: {Status}",
              command.OrderId, command.UserId, command.Status);
            _logger.LogInformation("------------------------------------------------");

            return Task.CompletedTask;
        }
    }
}