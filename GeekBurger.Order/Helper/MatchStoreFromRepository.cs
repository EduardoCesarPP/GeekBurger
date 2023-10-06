using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Products.Contract.Repository;
using GeekBurger.Service.Contract;

namespace GeekBurger.Products.Helper
{
    public class MatchStoreFromRepository : IMappingAction<OrderToUpsert, Order>

    {
        private IStoreRepository _storeRepository;

        public MatchStoreFromRepository(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public void Process(OrderToUpsert source, Order destination, ResolutionContext context)
        {
            var store = _storeRepository.GetStoreByName(source.StoreName);
            if (store != null)
                destination.StoreId = store.StoreId;
        }
    }
}