namespace GeekBurger.Service.Contract
{
    public class OrderToUpsert
    {
        public Guid OrderId { get; set; }
        public string StoreName { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}
