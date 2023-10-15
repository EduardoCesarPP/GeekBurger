namespace GeekBurger.Service.Contract
{
    public class OrderToGet
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }
    }
}
