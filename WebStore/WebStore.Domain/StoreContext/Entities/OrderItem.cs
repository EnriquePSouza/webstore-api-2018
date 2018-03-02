using System;
using FluentValidator;

namespace WebStore.Domain.StoreContext.Entities
{
    public class OrderItem : Notifiable
    {
        public OrderItem(Nullable<Guid> id,Order order, Product product, decimal quantity)
        {
            Id = id == null ? Guid.NewGuid() : id;
            Order = order;
            Product = product;
            Quantity = quantity;
            Price = product.Price;

            if (product.QuantityOnHand < quantity)
                AddNotification("Quantity", "Produto fora de estoque");

            product.DecreaseQuantity(quantity);
        }

        public Nullable<Guid> Id { get; private set; }
        public Order Order { get; set; }
        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}