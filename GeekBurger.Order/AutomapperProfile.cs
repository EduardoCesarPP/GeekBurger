using AutoMapper;
using GeekBurger.Service.Contract;
using GeekBurger.Shared.Helper;
using GeekBurger.Shared.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Order
{
    public class AutomapperProfile2 : Profile
    {
        public AutomapperProfile2()
        {
            CreateMap<OrderToUpsert, Shared.Model.Order>()
               .AfterMap<MatchOrderStoreFromRepository>();
            CreateMap<ProductOrder, OrderProduct>();
            CreateMap<OrderProduct, ProductOrder>();
            CreateMap<PaymentToUpsert, Payment>();
            CreateMap<Shared.Model.Order, OrderToGet>();

            CreateMap<EntityEntry<Shared.Model.Order>, OrderChangedMessage>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Entity));
            CreateMap<EntityEntry<Shared.Model.Order>, OrderChangedEvent>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Entity));
        }
    }
}
