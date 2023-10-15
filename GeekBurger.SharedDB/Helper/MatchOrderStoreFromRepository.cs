using AutoMapper;
using GeekBurger.Service.Contract;
using GeekBurger.Shared.ExternalRepositories;

namespace GeekBurger.Shared.Helper
{
    public class MatchOrderStoreFromRepository : IMappingAction<OrderToUpsert, Order>

    {
        private IStoreRepository _storeRepository;

        public MatchOrderStoreFromRepository(IStoreRepository storeRepository)
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