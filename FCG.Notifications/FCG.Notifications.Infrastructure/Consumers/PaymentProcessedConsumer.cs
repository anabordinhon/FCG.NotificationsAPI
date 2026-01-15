using FCG.Notifications.Application.Common.Ports;
using FCG.Notifications.Application.Notifications.UseCases.Commands.SendPurchaseConfirmationEmail;
using FCG.Payments.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Infrastructure.Consumers;
public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly ICommandHandler<SendPurchaseConfirmationEmailCommand> _handler;
    private readonly ILogger<PaymentProcessedConsumer> _logger;

    public PaymentProcessedConsumer(
        ICommandHandler<SendPurchaseConfirmationEmailCommand> handler,
        ILogger<PaymentProcessedConsumer> logger)
    {
        _handler = handler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        _logger.LogInformation("[Infra] ENTROU no PaymentProcessedConsumer");

        var message = context.Message;

        if (message.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
        {
            var command = new SendPurchaseConfirmationEmailCommand(
                message.OrderId,
                message.UserId,
                message.Status);

            await _handler.Handle(command, context.CancellationToken);

            _logger.LogInformation("[Infra] Evento processado e comando enviado para Application.");
        }
        else
        {
            _logger.LogWarning($"[Infra] Pagamento {message.OrderId} rejeitado. Nenhuma notificação enviada.");
        }
    }
}
