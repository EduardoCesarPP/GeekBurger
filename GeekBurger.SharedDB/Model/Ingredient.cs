using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Shared.Model
{
    public class Ingredient
    {
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        [Key]
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public List<Allergen> Allergens { get; set; } = new List<Allergen>();
    }

}
