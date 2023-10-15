using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Shared.Model
{
    public class FoodRestrictions
    {
        [ForeignKey("UserId")]
        [Key]
        public string CPF { get; set; }
        public User User { get; set; }
        public FoodCharacteristics Restriction { get; set; }
    }
}
