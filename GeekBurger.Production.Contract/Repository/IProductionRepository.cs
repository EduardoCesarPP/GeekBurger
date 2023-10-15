using GeekBurger.Service.Contract;
using GeekBurger.Shared.Model;

namespace GeekBurger.Production.Contract.Repository
{
    public interface IProductionRepository
    {
        public Task<List<OrderToUpsert>> CheckNewOrders();
        public Task<List<OrderChange>> CheckOrderChanges();
    }
}