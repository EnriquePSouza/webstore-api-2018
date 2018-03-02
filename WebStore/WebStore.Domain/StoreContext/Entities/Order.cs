using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using FluentValidator.Validation;
using WebStore.Domain.StoreContext.Enums;

namespace WebStore.Domain.StoreContext.Entities
{
    public class Order : Notifiable
    {
        private readonly IList<OrderItem> _items;

        public Order(Nullable<Guid> id, Customer customer, decimal deliveryFee, decimal discount)
        {
            Id = id == null ? Guid.NewGuid() : id;
            Customer = customer;
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Status = EOrderStatus.Created;
            DeliveryFee = deliveryFee;
            Discount = discount;
            _items = new List<OrderItem>();

            AddNotifications(new ValidationContract()
                .Requires()
                .IsGreaterThan(DeliveryFee,0,"DeliveryFee","Taxa de Entrega não informada")
                .IsGreaterThan(Discount, -1,"Discount","O Desconto não pode ser menor que zero")
            );
        }
        public Nullable<Guid> Id { get; private set; }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public decimal Discount { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();

        public decimal SubTotal() => Items.Sum(x => x.Total());
        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            AddNotifications(item.Notifications);
            if (item.Valid)
                _items.Add(item);
        }
    }
}