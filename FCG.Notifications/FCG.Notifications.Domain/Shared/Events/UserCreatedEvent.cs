namespace FCG.Notifications.Domain.Shared.Events
{
    public record UserCreatedEvent(Guid UserId, string Email, string FullName, Guid EventId, Guid CorrelationId);

}
