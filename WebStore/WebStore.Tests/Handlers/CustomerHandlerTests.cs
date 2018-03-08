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
            _command = new RegisterCustomerCommand();
            _command.Id = new Guid("74d96684-817d-4b5a-8edc-1a20aca2228c"); 
            _command.FirstName = "Enrique"; 
            _command.LastName = "Souza"; 
            _command.Email = "enrique@gmail.com";
            _command.UserId = new Guid("96352cd9-f793-42b1-bcb8-2f9c8698b330");
            _command.Password = "1234567890";
            _command.ConfirmPassword = "1234567890";
        }

        [TestMethod]
        public void ShouldRegisterCustomerWhenDocumentNotExistis()
        {
            _command.Document = "46718115533";
            _command.Username = "enrique";
            var handler = new CustomerHandler(new MockCustomerRepository());
            var result = handler.Handle(_command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }

        [TestMethod]
        public void ShouldNotRegisterCustomerWhenDocumentExists()
        {
            _command.Document = "22328792910";
            _command.Username = "enrique";
            var handler = new CustomerHandler(new MockCustomerRepository());
            var result = handler.Handle(_command);

            Assert.AreEqual(null, result);
            Assert.AreNotEqual(true, handler.Valid);
        }

        [TestMethod]
        public void ShouldRegisterCustomerWhenUsernameNotExistis()
        {
            _command.Document = "46718115533";
            _command.Username = "enrique";
            var handler = new CustomerHandler(new MockCustomerRepository());
            var result = handler.Handle(_command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }

        [TestMethod]
        public void ShouldNotRegisterCustomerWhenUsernameExistis()
        {
            _command.Document = "46718115533";
            _command.Username = "maria";
            var handler = new CustomerHandler(new MockCustomerRepository());
            var result = handler.Handle(_command);

            Assert.AreEqual(null, result);
            Assert.AreNotEqual(true, handler.Valid);
        }
    }
}