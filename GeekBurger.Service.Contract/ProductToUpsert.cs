﻿namespace GeekBurger.Service.Contract
{
    public class ProductToUpsert
    {
        public string StoreName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<ItemToUpsert> Items { get; set; }
    }
}