using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class AuthenticateUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}