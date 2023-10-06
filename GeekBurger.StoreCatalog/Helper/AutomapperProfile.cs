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
            CreateMap<Ingredient, ItemToGet>();
            CreateMap<Product, StoreCatalog>()
                .ForMember(c => c.StoreName, opt => opt.MapFrom(product => product.Store.Name));
        }
    }
}
