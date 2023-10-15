using AutoMapper;
using GeekBurger.Service.Contract;
using GeekBurger.Shared.ExternalRepositories;

namespace GeekBurger.Shared.Helper
{
    public class MatchIngredientsFromRepository : IMappingAction<IngredientToUpsert, Ingredient>
    {
        private IProductsRepository _productRepository;
        public MatchIngredientsFromRepository(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Process(IngredientToUpsert source, Ingredient destination, ResolutionContext context)
        {
            var fullListOfIngredients = _productRepository.GetFullListOfIngredients();
            var IngredientFound = fullListOfIngredients?.FirstOrDefault(Ingredient => Ingredient.Name.Equals(source.Name, StringComparison.InvariantCultureIgnoreCase));
            if (IngredientFound != null)
                destination.IngredientId = IngredientFound.IngredientId;
            else
                destination.IngredientId = Guid.NewGuid();
        }
    }
}