using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Shared.Model
{
    public class OrderChangedEvent
    {
        [Key]
        public Guid EventId { get; set; }

        public OrderState State { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public bool MessageSent { get; set; }
    }
}