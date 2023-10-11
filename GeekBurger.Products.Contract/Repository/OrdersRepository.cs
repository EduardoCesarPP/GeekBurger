using GeekBurger.Products.Contract.Model;
using GeekBurger.Products.Contract.Service;

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
            order.OrderId = Guid.NewGuid();
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
            var lista = _context.ChangeTracker.Entries<Order>();
            _orderChangedService
                .AddToMessageList(lista);
            _context.SaveChanges();
            _orderChangedService.SendMessagesAsync();

        }
    }
}
