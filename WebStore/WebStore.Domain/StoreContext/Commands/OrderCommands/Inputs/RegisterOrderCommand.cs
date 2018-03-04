using System;
using System.Collections.Generic;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class RegisterOrderCommand : ICommand
    {
        public Nullable<Guid> Id { get; set; }
        public Nullable<Guid> CustomerId { get; set; }
        public string Password { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<RegisterOrderItemCommand> Items { get; set; }
        public bool RegisteredCustomer => true;
    }
}