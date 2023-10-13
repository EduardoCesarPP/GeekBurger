namespace GeekBurger.Production.Contract.Service
{
    public interface ILogService
    {
        void SendMessagesAsync(string message);
    }
}