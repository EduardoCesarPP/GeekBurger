
using System;
using GeekBurger.Shared.Model;

namespace GeekBurger.Products.Repository
{
    public interface IOrderChangedEventRepository
    {
        OrderChangedEvent Get(Guid eventId);
        bool Add(OrderChangedEvent productChangedEvent);
        bool Update(OrderChangedEvent productChangedEvent);
        void Save();
    }
}