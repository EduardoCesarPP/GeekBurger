namespace GeekBurger.Shared.Model
{
    public class OrderChange
    {
        public Guid OrderId { get; set; }
        public string StoreName { get; set; }
        public OrderState State { get; set; }
    }
}
