using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Domain.StoreContext.CustomerCommands.Inputs;

namespace WebStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Enrique";
            command.LastName = "Souza";
            command.Document = "28659170377";
            command.Email = "enrique@gmail.com";
            command.Phone = "24994567879";

            Assert.AreEqual(true, command.IsValid());
        }
    }
}