﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Service.Contract
{
    public class OrderProduct
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
