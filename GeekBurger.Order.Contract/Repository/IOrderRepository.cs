using GeekBurger.Products.Contract;

using Microsoft.EntityFrameworkCore;
using GeekBurger.Shared.Model;
using GeekBurger.Service.Contract;

public interface IOrderRepository
{
    public bool Add(Order order);
    public void Save();
    public Task<List<OrderToUpsert>> CheckNewOrders();
}
