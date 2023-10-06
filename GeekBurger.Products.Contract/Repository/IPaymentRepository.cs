using GeekBurger.Products.Contract.Model;

public interface IPaymentRepository
{
    public bool Add(Payment payment);
    public void Save();
}
