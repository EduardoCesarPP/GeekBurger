using AutoMapper;
using GeekBurger.Service.Contract;
using GeekBurger.Shared.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Production.Helper
{
    public class ProductionAutomapperProfile : Profile
    {
        public ProductionAutomapperProfile()
        {
            CreateMap<OrderToUpsert, Order>();
            CreateMap<OrderChange, Order>();
        }
    }
}
