using AutoMapper;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;

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
        }
    }
}
