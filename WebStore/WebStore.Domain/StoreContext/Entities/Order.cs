using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using WebStore.Domain.StoreContext.Enums;

namespace WebStore.Domain.StoreContext.Entities
{
    public class Order : Notifiable
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;
        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

         public void AddItem(Product product, decimal quantity)
        {
            if(quantity > product.QuantityOnHand)
                AddNotification("OrderItem", $"Produto {product.Title} não tem {quantity} itens em estoque.");

            var item = new OrderItem(product, quantity);
            _items.Add(item);
        }

        //Create Order
        public void Place()
        {
            // Generate Order Number
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            if(_items.Count == 0)
                // FluentMethod
                AddNotification("Order", "Este pedido não possui itens.");
        }

        // Pay Order
        public void Pay()
        {
            Status = EOrderStatus.Paid;
        }
        // Send Order
        public void Ship()
        {
            // Every 5 products is a delivery
            var deliveries = new List<Delivery>();
            var count = 1;

            // Break Deliveries
            foreach (var item in _items)
            {
                if (count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
                count++;
            }
            // Send all Deliveries
            deliveries.ForEach(x => x.Ship());

            // Add all Deliveries to the Order
            deliveries.ForEach(x => _deliveries.Add(x));
        }

        // Cancel Order
        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
            _deliveries.ToList().ForEach(x => x.Cancel());
        }

    }
}