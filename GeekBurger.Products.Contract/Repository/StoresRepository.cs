using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Repository
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
