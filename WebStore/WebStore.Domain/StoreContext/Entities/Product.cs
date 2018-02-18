namespace WebStore.Domain.StoreContext.Entities 
{
    public class Product 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        // Stock quantity stored directly in the product.
        public string QuantityOnHand { get; set; }
    }
}