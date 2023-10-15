using System.Collections.Generic;
using Microsoft.Extensions.Hosting;

namespace GeekBurger.Production.Contract.Service
{
    public interface IProductionService : IHostedService
    {
        public Task<string>? CheckOrderChanges();
        public Task<string>? CheckNewOrders();
    }
}