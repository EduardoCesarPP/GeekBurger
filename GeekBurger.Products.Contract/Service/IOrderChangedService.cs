
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using GeekBurger.Products.Contract.Model;

namespace GeekBurger.Products.Contract.Service
{
    public interface IOrderChangedService : IHostedService
    {
        void SendMessagesAsync();
        void AddToMessageList(IEnumerable<EntityEntry<Order>> changes);
    }
}