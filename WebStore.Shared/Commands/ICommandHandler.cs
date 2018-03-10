namespace WebStore.Shared.Commands
{
    // Interface Used to Create Handlers
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}