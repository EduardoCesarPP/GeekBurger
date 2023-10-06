using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using Microsoft.EntityFrameworkCore;

public interface IProductsRepository
{
    IEnumerable<Product> GetProductsByStoreName(string storeName);
    public bool Add(Product product);
    public void Save();
    public Product GetProductById(Guid id);
    public IEnumerable<Ingredient> GetFullListOfItems();
    public IEnumerable<Product> GetProductsByRestrictions(List<FoodCharacteristics> restrictions);
}

