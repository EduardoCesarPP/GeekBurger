using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Products.Contract.Repository;

using GeekBurger.Service.Contract;

namespace GeekBurger.Products.Helper
{
    public class MatchItemsFromRepository : IMappingAction<ItemToUpsert, Ingredient>
    {
        private IProductsRepository _productRepository;
        public MatchItemsFromRepository(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Process(ItemToUpsert source, Ingredient destination, ResolutionContext context)
        {
            var fullListOfItems = _productRepository.GetFullListOfItems();
            var itemFound = fullListOfItems?.FirstOrDefault(item => item.Name.Equals(source.Name, StringComparison.InvariantCultureIgnoreCase));
            if (itemFound != null)
                destination.ItemId = itemFound.ItemId;
            else
                destination.ItemId = Guid.NewGuid();
        }
    }
}