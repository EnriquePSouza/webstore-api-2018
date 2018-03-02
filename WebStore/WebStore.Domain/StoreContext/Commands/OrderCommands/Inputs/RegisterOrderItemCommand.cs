using System;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class RegisterOrderItemCommand : ICommand
    {
        public Nullable<Guid> Id { get; set; }
        public Nullable<Guid> ProductId { get; set; }
        public int Quantity { get; set; }
    }
}