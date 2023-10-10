using GeekBurger.Products.Contract;

namespace GeekBurger.Service.Contract;

public class ProductChangedMessage
{
    public ProductState State { get; set; }
    public ProductToGet Product { get; set; }
}