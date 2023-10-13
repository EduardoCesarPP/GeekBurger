using AutoMapper;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;

namespace GeekBurger.StoreCatalogs.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductToGet>();
            CreateMap<Ingredient, IngredientToGet>();
            CreateMap<Allergen, AllergenToGet>();
            CreateMap<FoodCharacteristics, FoodCharacteristicsToGet>();

            CreateMap<ProductToGet, Product>();
            CreateMap<IngredientToGet, Ingredient>();
            CreateMap<AllergenToGet, Allergen>();
            CreateMap<FoodCharacteristicsToGet, FoodCharacteristics>();


            CreateMap<Product, StoreCatalog>()
                .ForMember(c => c.StoreName, opt => opt.MapFrom(product => product.Store.Name));
        }
    }
}
