using System;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class UpdateCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}