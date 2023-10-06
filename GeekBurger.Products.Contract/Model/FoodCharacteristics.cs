using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Model
{
    public class FoodCharacteristics
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<FoodRestrictions> Restrictions { get; set; } = new List<FoodRestrictions>();
        public List<Allergen> Allergens { get; set; } = new List<Allergen>();

    }
}
