
using GeekBurger.Products.Contract.Service;
using GeekBurger.Shared;
using GeekBurger.Shared.Model;

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
            var order = GetOrder(payment.OrderId); ;

            order.OrderState = OrderState.Paid;
            return true;
        }

        private Order GetOrder(Guid orderId)
        {
            return _context.Orders.Where(order => order.OrderId == orderId).FirstOrDefault();
        }

        public void Register(Payment payment, string storeName)
        {
            if (PedidoFinalizado(payment.OrderId))
                return;
            Add(payment);
            OrderChange orderChange = new OrderChange { OrderId = payment.OrderId, StoreName = storeName, State = OrderState.Paid };
            Save(orderChange);
        }

        private bool PedidoFinalizado(Guid orderId)
        {
            return GetOrder(orderId).OrderState != null;
        }

        public void Save(OrderChange orderChange)
        {
            _context.SaveChanges();
            _orderChangedService.SendMessagesAsync(orderChange);
        }
    }


}
