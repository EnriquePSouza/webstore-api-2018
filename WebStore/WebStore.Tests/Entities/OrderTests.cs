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
        private Product _keyboard;
        private Product _chair;
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

            Guid mouseId = new Guid("73319ab1-21a7-4fb7-9392-138ea772ef7a");
            Guid monitorId = new Guid("f712abcf-71f3-4b0c-9d73-13d31f4001c7");
            Guid userId = new Guid("96352cd9-f793-42b1-bcb8-2f9c8698b330");
            Guid customerId = new Guid("84eb7ce1-de34-4687-8bb4-91d0a019318b");
            Guid orderId = new Guid("6d44e254-892c-4f09-9e9a-888fb254e7fb");
            Guid orderItemOneId = new Guid("63e6cab0-cc05-43a3-a88a-244a87ebf151");
            Guid orderItemTwoId = new Guid("5ea0b425-72c5-41b3-9c50-fe62227f5008");

            _mouse = new Product(mouseId, "Mouse Gamer", "mouse.jpg", 100M, 10);
            _monitor = new Product(monitorId, "Monitor Gamer", "monitor.jpg", 100M, 10);

            _user = new User(userId, "enrique", "1234567890", true);
            _customer = new Customer(customerId, name, document, email, _user);
            _order = new Order(orderId, _customer, 5, 2);
            _orderItemOne = new OrderItem(orderItemOneId, _order, _mouse, 5);
            _orderItemTwo = new OrderItem(orderItemTwoId, _order, _monitor, 5);
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