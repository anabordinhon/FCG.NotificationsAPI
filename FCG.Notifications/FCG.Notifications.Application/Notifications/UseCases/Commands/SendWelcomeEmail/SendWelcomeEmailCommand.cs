namespace FCG.Notifications.Application.Notifications.UseCases.Commands.SendWelcomeEmail;
public record SendWelcomeEmailCommand(Guid UserId, string Email, string FullName);
