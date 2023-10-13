using AutoMapper;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Products.Contract.Repository;
using GeekBurger.Service.Contract;
using Microsoft.AspNetCore.Mvc;

[Route("api/order")]

public class OrderController : Controller
{

    private IOrderRepository _orderRepository;

    private IMapper _mapper;

    public OrderController(IOrderRepository orderRepository, IMapper mapper)

    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    [HttpPost()]
    public IActionResult AddOrder([FromBody] OrderToUpsert orderToAdd)
    {
        if (orderToAdd == null)
            return BadRequest();

        var order = _mapper.Map<Order>(orderToAdd);
        if (order.StoreId == Guid.Empty)
            return new UnprocessableEntityResult();
        _orderRepository.Add(order);
        _orderRepository.Save();

        var total = 0M;
        orderToAdd.Products.ForEach(p => { total += p.Price; });
        var orderToGet = new OrderToGet { OrderId = order.OrderId, StoreName = order.Store.Name, Price = total };
        return Ok(orderToGet);
    }


}
