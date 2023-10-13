
using AutoMapper;
using GeekBurger.Production.Contract.Repository;
using GeekBurger.Products.Contract.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/order")]

public class ProductionController : Controller
{

    private IProductionRepository _productionRepository;

    private IMapper _mapper;

    public ProductionController(IProductionRepository productionRepository, IMapper mapper)

    {
        _productionRepository = productionRepository;
        _mapper = mapper;
    }

    private List<Order> PedidosAtivos = new List<Order>();
    private List<Order> PedidosFinalizados = new List<Order>();

    [HttpPost()]
    public async Task<IActionResult> ProductionService()
    {
        while (true)
        {
            bool AchouNovos = false;
            bool AchouAlteracao = false;

            var NovosPedidos = _mapper.Map<List<Order>>(await _productionRepository.CheckOrders());
            if (NovosPedidos is not null)
            {
                NovosPedidos.ForEach(order =>
                {
                    PedidosAtivos.Add(order);
                });
                AchouNovos = true;
            }

            var Alteracoes = _mapper.Map<List<Order>>(await _productionRepository.CheckOrderChanges());
            if (Alteracoes is not null)
            {
                AchouAlteracao = true;
                foreach (Order order in Alteracoes)
                {
                    var alterado = PedidosAtivos.Where(o => o.OrderId == order.OrderId).FirstOrDefault();
                    alterado.OrderState = order.OrderState;
                    PedidosFinalizados.Add(alterado);
                    PedidosAtivos.Remove(alterado);
                }
            }

            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("Pedidos Ativos:");
            if (AchouNovos)
            {
                PedidosAtivos.ForEach(o =>
                    {
                        Console.WriteLine(o.ToString());
                    });
            }
            else
            {
                Console.WriteLine("Sem Alteração");
            }

            Console.WriteLine("Pedidos Finalizados:");
            if (AchouNovos)
            {
                PedidosFinalizados.ForEach(o =>
                    {
                        Console.WriteLine(o.ToString());
                    });
            }
            else
            {
                Console.WriteLine("Sem Alteração");
            }
            Console.WriteLine("-------------------------------------------------------------");

        }
        return Ok();
    }


}
