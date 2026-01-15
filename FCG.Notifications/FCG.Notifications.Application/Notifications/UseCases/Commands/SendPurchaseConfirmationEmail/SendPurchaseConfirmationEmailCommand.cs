namespace FCG.Notifications.Application.Notifications.UseCases.Commands.SendPurchaseConfirmationEmail;

public record SendPurchaseConfirmationEmailCommand(Guid OrderId, int UserId, string Status);