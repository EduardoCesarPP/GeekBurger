using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Products.Contract.Model
{
    public class Product
    {
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
        [Key]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<ProductOrder> Orders { get; set; } = new List<ProductOrder>();
    }
}