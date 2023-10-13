using GeekBurger.Products.Contract.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Model
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
