using AutoMapper;

using GeekBurger.Production.Contract.Service;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;
using System.Text;

namespace GeekBurger.Production.Contract.Repository
{
    public class ProductionRepository : IProductionRepository
    {
        private IMapper _mapper;
        private IProductionService _productionService;
        public ProductionRepository(IProductionService productionervice, IMapper mapper)
        {
            _productionService = productionervice;
            _mapper = mapper;
        }

        public async Task<List<OrderToGet>> CheckOrderChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderToGet>> CheckOrders()
        {
            throw new NotImplementedException();
        }
    }
}
