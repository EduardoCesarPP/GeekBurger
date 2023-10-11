using GeekBurger.Products.Contract.Model;
using System;

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