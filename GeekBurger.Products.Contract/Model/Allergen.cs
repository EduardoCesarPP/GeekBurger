using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Products.Contract.Model
{
    public class Allergen
    {
        [ForeignKey("ItemId")]
        [Key]
        public Guid ItemId { get; set; }
        public Ingredient Ingredient { get; set; }
        public FoodCharacteristics Characteristic { get; set; }
    }
}