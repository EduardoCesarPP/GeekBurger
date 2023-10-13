using AutoMapper;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;

namespace GeekBurger.Products.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductToGet>();
            CreateMap<Ingredient, IngredientToGet>();
            CreateMap<Product, StoreCatalog>()
                .ForMember(c => c.StoreName, opt => opt.MapFrom(product => product.Store.Name));
            CreateMap<ProductToUpsert, Product>().AfterMap<MatchStoreFromRepository>();
            CreateMap<IngredientToUpsert, Ingredient>().AfterMap<MatchIngredientsFromRepository>();

            CreateMap<PaymentToUpsert, Payment>();
        }
    }
}
