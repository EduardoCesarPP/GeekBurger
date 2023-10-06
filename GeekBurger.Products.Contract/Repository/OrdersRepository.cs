using GeekBurger.Products.Contract.Model;

namespace GeekBurger.Products.Contract.Repository
{
    public class OrdersRepository : IOrderRepository
    {
        private ProductsDbContext _context;
        public OrdersRepository(ProductsDbContext context)
        {
            _context = context;
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
            _context.SaveChanges();
        }
    }
}
