using AutoMapper;
using GeekBurger.Service.Contract;
using GeekBurger.Shared.ExternalRepositories;

namespace GeekBurger.Shared.Helper
{
    public class MatchProductStoreFromRepository : IMappingAction<ProductToUpsert, Product>

    {
        private IStoreRepository _storeRepository;

        public MatchProductStoreFromRepository(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public void Process(ProductToUpsert source, Product destination, ResolutionContext context)
        {
            var store = _storeRepository.GetStoreByName(source.StoreName);
            if (store != null)
                destination.StoreId = store.StoreId;
        }
    }
}