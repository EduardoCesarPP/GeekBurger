using AutoMapper;
using GeekBurger.Service.Contract;

public class OrderBackgroundService : BackgroundService
{
    private IOrderRepository _orderRepository;

    private IMapper _mapper;

    public OrderBackgroundService(IOrderRepository orderRepository, IMapper mapper)

    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var NovosPedidos = _mapper.Map<List<Order>>(await _orderRepository.CheckNewOrders());
            if (NovosPedidos.Count > 0)
            {
                NovosPedidos.ForEach(order =>
                {
                    if (order.StoreId != Guid.Empty)
                    {
                        _orderRepository.Add(order);
                        _orderRepository.Save();
                    }
                });
            }
        }
    }
}