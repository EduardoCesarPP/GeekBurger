using GeekBurger.Products.Contract.Model;
using GeekBurger.Products.Contract.Service;

namespace GeekBurger.Products.Contract.Repository
{
    public class PaymentsRepository : IPaymentRepository
    {
        private ProductsDbContext _context;
        private IOrderChangedService _orderChangedService;
        public PaymentsRepository(ProductsDbContext context, IOrderChangedService orderChangedService)
        {
            _context = context;
            _orderChangedService = orderChangedService;
        }
        public bool Add(Payment payment)

        {
            payment.PaymentId = Guid.NewGuid();
            _context.Payments.Add(payment);
            var order = _context.Orders.Where(order => order.OrderId == payment.OrderId).FirstOrDefault();
            order.OrderState = OrderState.Paid;
            return true;
        }
        public void Save()
        {
            _orderChangedService.AddToMessageList(_context.ChangeTracker.Entries<Order>());
            _context.SaveChanges();
            _orderChangedService.SendMessagesAsync();
        }
    }


}
