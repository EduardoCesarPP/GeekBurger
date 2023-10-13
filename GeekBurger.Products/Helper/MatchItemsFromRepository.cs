using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Products.Contract.Repository;

using GeekBurger.Service.Contract;

namespace GeekBurger.Products.Helper
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