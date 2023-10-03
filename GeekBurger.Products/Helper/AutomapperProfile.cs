﻿using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Service.Contract;

namespace GeekBurger.Products.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductToGet>();
            CreateMap<Item, ItemToGet>();
            CreateMap<ProductToUpsert, Product>().AfterMap<MatchStoreFromRepository>();

            CreateMap<ItemToUpsert, Item>()            .AfterMap<MatchItemsFromRepository>();
        }
    }
}
