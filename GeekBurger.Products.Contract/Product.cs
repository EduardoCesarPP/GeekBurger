namespace GeekBurger.Products.Contract
{
    public class Product
    {
        public string StoreName { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Item> Ingredients { get; set; }
        public double Price { get; set; }

    }
}