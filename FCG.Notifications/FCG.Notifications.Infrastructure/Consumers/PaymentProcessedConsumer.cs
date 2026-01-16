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
        var message = context.Message;

        _logger.LogInformation(
                "Iniciando consumo de PaymentProcessedEvent | OrderId: {OrderId} | Status: {Status}",
                message.OrderId,
                message.Status);

        if (message.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
        {
            var command = new SendPurchaseConfirmationEmailCommand(
                message.OrderId,
                message.UserId,
                message.Status);

            await _handler.Handle(command, context.CancellationToken);


            _logger.LogInformation(
                        "PaymentProcessedEvent processado | OrderId: {OrderId} | UserId: {UserId}",
                        message.OrderId,
                        message.UserId
                    );
        }
        else
        {
            _logger.LogWarning(
                        "Notificação não enviada. Motivo: Pagamento com status não aprovado | OrderId: {OrderId} | Status: {Status}",
                        message.OrderId,
                        message.Status);
        }
    }
}
