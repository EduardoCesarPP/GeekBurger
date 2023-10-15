using AutoMapper;
using GeekBurger.Shared.Model;
using GeekBurger.Production.Contract.Service;
using GeekBurger.Service.Contract;
using Newtonsoft.Json;

namespace GeekBurger.Production.Contract.Repository
{
    public class ProductionRepository : IProductionRepository
    {
        private IProductionService _productionService;
        public ProductionRepository(IProductionService productionervice)
        {
            _productionService = productionervice;
        }

        public async Task<List<OrderChange>> CheckOrderChanges()
        {
            var orders = JsonConvert.DeserializeObject<List<OrderChange>>(await _productionService.CheckOrderChanges());
            return orders;
        }

        public async Task<List<OrderToUpsert>> CheckNewOrders()
        {
            var orders = JsonConvert.DeserializeObject<List<OrderToUpsert>>(await _productionService.CheckNewOrders());
            return orders;
        }

    }
}
