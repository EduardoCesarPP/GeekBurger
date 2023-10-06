using GeekBurger.Products.Contract.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.Contract.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private ProductsDbContext _context;
        public UsersRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FoodCharacteristics> GetRestrictions(string cpf)
        {
            List<FoodCharacteristics> foodCharacteristics = new List<FoodCharacteristics>();
            _context.Users.Where(u => u.CPF == cpf).FirstOrDefault().Restrictions.ForEach(r =>
            {
                foodCharacteristics.Add(r.Restriction);
            });
            return foodCharacteristics;
        }

       
    }
}
