using AutoMapper;

using GeekBurger.Production.Contract.Service;
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

        
    }
}
