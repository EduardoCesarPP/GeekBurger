namespace GeekBurger.Service.Contract
{
    public class ProductToUpsert
    {
        public string StoreName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public List<IngredientToUpsert> Ingredients { get; set; }
    }
}
