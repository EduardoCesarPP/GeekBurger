using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using Microsoft.EntityFrameworkCore;

public interface IOrderRepository
{
    public bool Add(Order order);
    public void Save();
}
