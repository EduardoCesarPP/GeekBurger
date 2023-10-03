using GeekBurger.Products.Contract;
using Microsoft.EntityFrameworkCore;

public interface IStoreRepository
{
    public Store GetStoreByName(string storeName);
}

