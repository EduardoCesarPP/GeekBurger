using GeekBurger.Service.Contract;

namespace GeekBurger.Shared.Model
{
    public class OrderChangedMessage
    {
        public OrderState State { get; set; }
        public OrderToGet Order { get; set; }
    }
}
