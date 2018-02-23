using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Enums;
using WebStore.Domain.StoreContext.ValueObjects;

namespace WebStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Product _keyboard;
        private Product _chair;
        private Product _monitor;
        private Product _mouse;
        private Customer _customer;
        private Order _order;

        public OrderTests()
        {
            var name = new Name("Enrique", "Souza");
            var document = new Document("46718115533");
            var email = new Email("enrique@gmail.com");
            _customer = new Customer(name, document, email, "551999876542");
            _order = new Order(_customer);
            _mouse = new Product("Mouse Gamer", "Mouse Gamer", "mouse.jpg", 100M, 10);
            _chair = new Product("Cadeira Gamer", "Cadeira Gamer", "cadeira.jpg", 100M, 10);
            _keyboard = new Product("Teclado Gamer", "Teclado Gamer", "teclado.jpg", 100M, 10);
            _monitor = new Product("Monitor Gamer", "Monitor Gamer", "monitor.jpg", 100M, 10);
        }

        [TestMethod]
        public void ShouldCreateOrderWhenValid()
        {
            Assert.AreEqual(true, _order.Valid);
        }

        [TestMethod]
        public void StatusShouldBeCreatedWhenOrderCreated()
        {
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
        }

        [TestMethod]
        public void ShouldReturnTwoWhenAddedTwoValidItems()
        {
            _order.AddItem(_monitor, 5);
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(2, _order.Items.Count);
        }

        [TestMethod]
        public void ShouldReturnFiveWhenAddedPurchasedFiveItem()
        {
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(_mouse.QuantityOnHand, 5);
        }

        [TestMethod]
        public void ShouldReturnANumberWhenOrderPlaced()
        {
            _order.Place();
            Assert.AreNotEqual("", _order.Number);
        }

        [TestMethod]
        public void ShouldReturnPaidWhenOrderPaid()
        {
            _order.Pay();
            Assert.AreEqual(EOrderStatus.Paid, _order.Status);
        }

        [TestMethod]
        public void ShouldTwoWhenPurchasedTenProducts()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();

            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        [TestMethod]
        public void StatusShouldBeCanceledWhenOrderCanceled()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        public void ShouldCancelShippingsWhenOrderCanceled()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();

            _order.Cancel();
            foreach (var x in _order.Deliveries)
            {
                Assert.AreEqual(EDeliveryStatus.Canceled, x.Status);
            }
        }
    }
}