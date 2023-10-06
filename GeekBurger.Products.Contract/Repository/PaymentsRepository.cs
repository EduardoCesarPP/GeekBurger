using GeekBurger.Products.Contract.Model;

namespace GeekBurger.Products.Contract.Repository
{
    public class PaymentsRepository : IPaymentRepository
    {
        private ProductsDbContext _context;
        public PaymentsRepository(ProductsDbContext context)
        {
            _context = context;
        }
        public bool Add(Payment payment)

        {
            payment.PaymentId = Guid.NewGuid();
            _context.Payments.Add(payment);
            return true;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }


}
