namespace GeekBurger.Shared.ExternalRepositories
{
    public class StoresRepository : IStoreRepository
    {
        private ProductsDbContext _context;
        public StoresRepository(ProductsDbContext context)
        {
            _context = context;
        }
        public Store GetStoreByName(string storeName)
        {
            return _context.Stores.Where(store => store.Name == storeName).FirstOrDefault();
        }
    }
}
