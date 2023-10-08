using GeekBurger.Products.Contract;

namespace GeekBurger.Products.Repository.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetProductsbyStoreName(string storeName);
    }
}
