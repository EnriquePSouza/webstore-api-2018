using WebStore.Domain.StoreContext.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Tests.Mocks;

namespace WebStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Enrique";
            command.LastName = "Souza";
            command.Document = "28659170377";
            command.Email = "enrique@gmail.com";
            command.Phone = "24987362982";

            var handler = new CustomerHandler(new MockCustomerRepository(), new MockEmailService());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }
    }
}