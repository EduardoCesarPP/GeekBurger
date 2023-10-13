using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Products.Contract.Model
{
    public class Allergen
    {
        [ForeignKey("IngredientId")]
        [Key]
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public FoodCharacteristics Characteristic { get; set; }
    }
}