using System;
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

        public OrderHandlerTests()
        {
            _command.Id = new Guid("4c169a85-782e-4214-87f7-8594cdcb8440");
            _command.CustomerId = new Guid("74d96684-817d-4b5a-8edc-1a20aca2228c");
            // The command need more informations to execute an correct test.
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