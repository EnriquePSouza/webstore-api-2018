using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Enums;
using WebStore.Domain.StoreContext.ValueObjects;

namespace WebStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Product _monitor;
        private Product _mouse;
        private Customer _customer;
        private Order _order;
        private User _user;
        private OrderItem _orderItemOne;
        private OrderItem _orderItemTwo;

        public OrderTests()
        {
            var name = new Name("Enrique", "Souza");
            var document = new Document("46718115533");
            var email = new Email("enrique@gmail.com");

            _mouse = new Product(Guid.NewGuid(), "Mouse Gamer", "mouse.jpg", 100M, 10);
            _monitor = new Product(Guid.NewGuid(), "Monitor Gamer", "monitor.jpg", 100M, 10);

            _user = new User(Guid.NewGuid(), "enrique", "1234567890", true);
            _customer = new Customer(Guid.NewGuid(), name, document, email, _user);
            _order = new Order(Guid.NewGuid(), _customer, 5, 2);
            _orderItemOne = new OrderItem(Guid.NewGuid(), _order, _mouse, 5);
            _orderItemTwo = new OrderItem(Guid.NewGuid(), _order, _monitor, 5);
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
            _order.AddItem(_orderItemOne);
            _order.AddItem(_orderItemTwo);
            Assert.AreEqual(2, _order.Items.Count);
        }

        [TestMethod]
        public void ShouldReturnFiveWhenAddedPurchasedFiveItem()
        {
            _order.AddItem(_orderItemOne);
            Assert.AreEqual(_mouse.QuantityOnHand, 5);
        }

        [TestMethod]
        public void ShouldReturnANumberWhenOrderCreated()
        {
            Assert.AreNotEqual("", _order.Number);
        }
    }
}