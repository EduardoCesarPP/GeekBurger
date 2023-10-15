using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Shared.Model
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        public string PayType { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public int SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int RequesterId { get; set; }
    }
}
