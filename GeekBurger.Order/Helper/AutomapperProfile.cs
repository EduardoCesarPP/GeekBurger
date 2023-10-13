using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Products.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<OrderToUpsert, Order>()
                .AfterMap<MatchStoreFromRepository>();
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
