using System;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class RegisterCustomerCommand : ICommand
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}