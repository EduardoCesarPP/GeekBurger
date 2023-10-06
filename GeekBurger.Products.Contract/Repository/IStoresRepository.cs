using GeekBurger.Products.Contract.Model;

namespace GeekBurger.Products.Contract.Repository
{
    public interface IStoreRepository
    {
        public Store GetStoreByName(string storeName);
    }
    }