namespace GeekBurger.Service.Contract
{
    public class PaymentToUpsert
    {
        public Guid OrderId { get; set; }
        public string StoreName { get; set; }
        public string PayType { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public int SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int RequesterId { get; set; }
    }
}
