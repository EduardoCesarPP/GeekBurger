using GeekBurger.Shared.Model;
using AutoMapper;
using GeekBurger.Production.Contract.Repository;
using GeekBurger.Service.Contract;

public class ProductionBackgroundService : BackgroundService
{

    private IProductionRepository _productionRepository;

    private IMapper _mapper;

    public ProductionBackgroundService(IProductionRepository productionRepository, IMapper mapper)

    {
        _productionRepository = productionRepository;
        _mapper = mapper;
    }

    private List<Order> PedidosAtivos = new List<Order>();
    private List<Order> PedidosFinalizados = new List<Order>();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            bool AchouNovos = false;
            bool AchouAlteracao = false;

            var NovosPedidosUpsert = (List<OrderToUpsert>)await _productionRepository.CheckNewOrders();
            if (NovosPedidosUpsert is not null)
            {
                List<Order> NovosPedidos = (NovosPedidosUpsert).Select(upsert =>
                {
                    Order order = new Order();
                    order.OrderId = upsert.OrderId;
                    order.OrderState = null;
                    order.Products = upsert.Products.Select(orderProduct =>
                    {
                        ProductOrder productOrder = new ProductOrder();
                        productOrder.ProductId = orderProduct.ProductId;
                        productOrder.Product = new Product { ProductId = orderProduct.ProductId, Price = orderProduct.Price };
                        return productOrder;
                    }).ToList();
                    return order;
                }).ToList();

                NovosPedidos.ForEach(order =>
                {
                    PedidosAtivos.Add(order);
                });
                AchouNovos = true;
            }

            var Alteracoes = _mapper.Map<List<Order>>(await _productionRepository.CheckOrderChanges());
            if (Alteracoes is not null && Alteracoes.Count > 0)
            {
                foreach (Order order in Alteracoes)
                {
                    var alterado = PedidosAtivos.Where(o => o.OrderId == order.OrderId).FirstOrDefault();
                    if (alterado is not null)
                    {
                        AchouAlteracao = true;
                        alterado.OrderState = order.OrderState;
                        PedidosFinalizados.Add(alterado);
                        PedidosAtivos.Remove(alterado);
                    }
                }
            }

            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("Pedidos Ativos:");
            if (AchouNovos)
            {
                PedidosAtivos.ForEach(o =>
                {
                    Console.WriteLine(o.ToString());
                    Console.WriteLine("-----");
                });
            }
            else
            {
                Console.WriteLine("Sem Alteração");
            }

            Console.WriteLine("Pedidos Finalizados:");
            if (AchouAlteracao)
            {
                PedidosFinalizados.ForEach(o =>
                {
                    Console.WriteLine(o.ToString());
                    Console.WriteLine("-----");
                });
            }
            else
            {
                Console.WriteLine("Sem Alteração");
            }
            Console.WriteLine("-------------------------------------------------------------");

        }
    }


}
