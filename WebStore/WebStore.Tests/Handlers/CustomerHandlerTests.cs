using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Tests.Mocks;

namespace WebStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        private RegisterCustomerCommand _command;

        public CustomerHandlerTests()
        {
            _command.Id = new Guid("ae3b97a6-a8c4-4979-acb4-02662981d1e1");
            _command.FirstName = "Enrique";
            _command.LastName = "Souza";
            _command.Email = "enrique@gmail.com";
            _command.Document = "28659170377";
            _command.UserId = new Guid("a127db39-991f-4ead-aa1f-80be68e083d0");
            _command.Username = "enrique";
            _command.Password = "1234567890";
            _command.ConfirmPassword = "1234567890";
        }

        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var handler = new CustomerHandler(new MockCustomerRepository());
            var result = handler.Handle(_command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }
    }
}