global using System.ComponentModel.DataAnnotations;

namespace GeekBurger.Products.Contract.Model
{
    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}