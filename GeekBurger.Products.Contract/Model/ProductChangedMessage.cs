using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;
using System.Collections.Generic;

namespace GeekBurger.Products.Contract
{
    public class OrderChangedMessage
    {
        public OrderState State { get; set; }
        public OrderToGet Order { get; set; }
    }
}
