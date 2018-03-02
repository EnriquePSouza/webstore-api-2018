using System;
using FluentValidator;

namespace WebStore.Domain.StoreContext.Entities
{
    public class Product : Notifiable
    {
        public Product(Nullable<Guid> id, string title, string image, decimal price, decimal quantity)
        {
            Id = id == null ? Guid.NewGuid() : id;
            Title = title;
            Image = image;
            Price = price;
            QuantityOnHand = quantity;
        }

        public Nullable<Guid> Id { get; private set; }
        public string Title { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        // Stock quantity stored directly in the product.
        public decimal QuantityOnHand { get; private set; }

        public override string ToString()
        {
            return Title;
        }

        public void DecreaseQuantity(decimal quantity) => QuantityOnHand -= quantity;
    }
}