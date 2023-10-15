using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using GeekBurger.Shared;
using GeekBurger.Shared.Model;

namespace GeekBurger.Products.Repository
{
    public class OrderChangedEventRepository : IOrderChangedEventRepository
    {
        private readonly ProductsDbContext _dbContext;

        public OrderChangedEventRepository(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderChangedEvent Get(Guid eventId)
        {
            return _dbContext.OrderChangedEvents
                .FirstOrDefault(product => product.EventId == eventId);
        }

        public bool Add(OrderChangedEvent productChangedEvent)
        {
            productChangedEvent.Order =
                _dbContext.Orders
                .FirstOrDefault(_ => _.OrderId == productChangedEvent.Order.OrderId);

            productChangedEvent.EventId = Guid.NewGuid();

            _dbContext.OrderChangedEvents.Add(productChangedEvent);

            return true;
        }

        public bool Update(OrderChangedEvent productChangedEvent)
        {
            return true;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

       
    }
}