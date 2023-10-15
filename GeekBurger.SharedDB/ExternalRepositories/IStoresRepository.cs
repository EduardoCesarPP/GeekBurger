namespace GeekBurger.Shared.ExternalRepositories
{
    public interface IStoreRepository
    {
        public Store GetStoreByName(string storeName);
    }
    }