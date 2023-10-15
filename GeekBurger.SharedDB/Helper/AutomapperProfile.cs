using AutoMapper;
using GeekBurger.Service.Contract;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Shared.Helper
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

            CreateMap<Product, ProductToGet>();
            CreateMap<Ingredient, IngredientToGet>();
            CreateMap<Product, StoreCatalog>()
                .ForMember(c => c.StoreName, opt => opt.MapFrom(product => product.Store.Name));
            CreateMap<ProductToUpsert, Product>().AfterMap<MatchProductStoreFromRepository>();
            CreateMap<IngredientToUpsert, Ingredient>().AfterMap<MatchIngredientsFromRepository>();

            CreateMap<PaymentToUpsert, Payment>();



            CreateMap<OrderToUpsert, Order>()
               .AfterMap<MatchOrderStoreFromRepository>();
            CreateMap<ProductOrder, OrderProduct>();
            CreateMap<OrderProduct, ProductOrder>();
            CreateMap<PaymentToUpsert, Payment>();
            CreateMap<Order, OrderToGet>();

            CreateMap<EntityEntry<Order>, OrderChangedMessage>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Entity));
            CreateMap<EntityEntry<Order>, OrderChangedEvent>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Entity));
        }
    }
}
