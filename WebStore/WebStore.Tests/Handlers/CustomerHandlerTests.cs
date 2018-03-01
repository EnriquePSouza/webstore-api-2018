using WebStore.Domain.StoreContext.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Tests.Mocks;
using WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;

namespace WebStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new RegisterCustomerCommand();
            command.FirstName = "Enrique";
            command.LastName = "Souza";
            command.Document = "28659170377";
            command.Email = "enrique@gmail.com";

            var handler = new CustomerHandler(new MockCustomerRepository());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }
    }
}