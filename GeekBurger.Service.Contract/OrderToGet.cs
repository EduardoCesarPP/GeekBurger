﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Service.Contract
{
    public class OrderToGet
    {
        public Guid OrderId { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }
    }
}
