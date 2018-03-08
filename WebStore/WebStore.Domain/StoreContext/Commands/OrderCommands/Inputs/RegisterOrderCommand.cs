using System;
using System.Collections.Generic;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class RegisterOrderCommand : ICommand
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<RegisterOrderItemCommand> Items { get; set; }
    }
}