
namespace GeekBurger.Shared.ExternalRepositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetProductsByStoreName(string storeName);
        public bool Add(Product product);
        public void Save();
        public Product GetProductById(Guid id);
        public IEnumerable<Ingredient> GetFullListOfIngredients();
        public IEnumerable<Product> GetProductsByRestrictions(List<FoodCharacteristics> restrictions);
    }
}