using GeekBurger.Products.Contract;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Products.Services;

public interface IProductChangedService: IHostedService
{
    void SendMessagesAsync();
    void AddToMessageList(IEnumerable<EntityEntry<Product>> changes);
}