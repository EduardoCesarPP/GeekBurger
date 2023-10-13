
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;

namespace GeekBurger.Production.Contract.Repository
{
    public interface IProductionRepository
    {
        public Task<List<OrderToGet>> CheckOrders();
        public Task<List<OrderToGet>> CheckOrderChanges();
    }
}