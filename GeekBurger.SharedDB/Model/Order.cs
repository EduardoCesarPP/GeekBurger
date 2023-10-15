using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Shared.Model
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public OrderState? OrderState { get; set; }
        public Guid StoreId { get; set; }
        public List<ProductOrder> Products { get; set; } = new List<ProductOrder>();



        public override string ToString()
        {
            return $"Pedido:\n" +
                $"Id: { OrderId }\n" +
                $"Situação: {OrderState}";
        }
    }
}
