
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;

using GeekBurger.Shared.Model;

namespace GeekBurger.Products.Contract.Service
{
    public interface IOrderChangedService : IHostedService
    {
        void SendMessagesAsync(OrderChange orderChange);
        public Task<string> CheckNewOrders();
    }
}