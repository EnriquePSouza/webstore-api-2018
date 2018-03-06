using System;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class RegisterOrderItemCommand : ICommand
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}