using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Tests.Mocks;

namespace WebStore.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private RegisterOrderCommand _command;
        private RegisterOrderItemCommand _orderItemCommand;
        private IList<RegisterOrderItemCommand> _orderItemsCommand;

        public OrderHandlerTests()
        {
            _orderItemCommand = new RegisterOrderItemCommand();
            _orderItemCommand.Id = new Guid("e52d95eb-0e4c-426b-88cc-6f040aa2a55a");
            _orderItemCommand.ProductId = new Guid("73319ab1-21a7-4fb7-9392-138ea772ef7a");
            _orderItemCommand.Quantity = 2;

            _orderItemsCommand = new List<RegisterOrderItemCommand>();
            _orderItemsCommand.Add(_orderItemCommand);
            
            _command = new RegisterOrderCommand();
            _command.Id = new Guid("4c169a85-782e-4214-87f7-8594cdcb8440");
            _command.CustomerId = new Guid("74d96684-817d-4b5a-8edc-1a20aca2228c");
            _command.DeliveryFee = 5;
            _command.Discount = 2;
            _command.Items = _orderItemsCommand.ToArray();
        }

        [TestMethod]
        public void ShouldRegisterOrderWhenCommandIsValid()
        {
            var handler = new OrderHandler(new MockCustomerRepository(),
                new MockProductRepository(), new MockOrderRepository());
            var result = handler.Handle(_command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }
    }
}