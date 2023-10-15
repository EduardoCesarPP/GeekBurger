
using GeekBurger.Products.Contract.Service;
using GeekBurger.Service.Contract;
using GeekBurger.Shared;
using GeekBurger.Shared.Model;
using Newtonsoft.Json;

namespace GeekBurger.Products.Contract.Repository
{
    public class OrdersRepository : IOrderRepository
    {
        private ProductsDbContext _context;
        private IOrderChangedService _orderChangedService;
        public OrdersRepository(ProductsDbContext context, IOrderChangedService orderChangedService)
        {
            _context = context;
            _orderChangedService = orderChangedService;
        }
        public bool Add(Order order)

        {
            var orders = _context.Orders.ToList();
            _context.Orders.Add(order);
            order.Products.ForEach(p =>
            {
                p.OrderId = order.OrderId;
                _context.ProductOrders.Add(p);
            });
            return true;
        }
        public void Save()
        {
            var orders = _context.Orders.ToList();
            _context.SaveChanges();
        }

        public async Task<List<OrderToUpsert>> CheckNewOrders()
        {
            var orders = JsonConvert.DeserializeObject<List<OrderToUpsert>>(await _orderChangedService.CheckNewOrders());
            return orders;
        }

    }
}
