using GeekBurger.Products.Contract.Model;

namespace GeekBurger.Products.Contract.Repository
{
    public interface IUsersRepository
    {
        public IEnumerable<FoodCharacteristics> GetRestrictions(string cpf);
    }
}


