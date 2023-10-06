using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Model
{
    public class User
    {
        [Key]
        public string CPF { get; set; }
        public List<FoodRestrictions> Restrictions { get; set; } = new List<FoodRestrictions>();
    }
}
