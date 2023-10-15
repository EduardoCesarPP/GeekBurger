
using GeekBurger.Shared.Model;

public interface IPaymentRepository
{
    public bool Add(Payment payment);
    public void Save(OrderChange orderChange);
    void Register(Payment payment, string storeName);
}
