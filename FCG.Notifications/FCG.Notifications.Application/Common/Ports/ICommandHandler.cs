namespace FCG.Notifications.Application.Common.Ports
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command,CancellationToken cancellationToken);
    }
}
