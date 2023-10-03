using GeekBurger.Products.Contract;
using Microsoft.EntityFrameworkCore;

public interface IProductsRepository
{
    IEnumerable<Product> GetProductsByStoreName(string storeName);
    public bool Add(Product product);
    public void Save();
    public Product GetProductById(Guid id);
    public IEnumerable<Item> GetFullListOfItems();
}

