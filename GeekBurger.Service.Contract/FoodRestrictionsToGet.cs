using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Model
{
    public class FoodRestrictionsToGet
    {
   
        public Guid UserId { get; set; }
        public List<string> Restrictions { get; set; }
    }
}
