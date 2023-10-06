using AutoMapper;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;
using Microsoft.AspNetCore.Mvc;

[Route("api/payment")]

public class PaymentController : Controller
{

    private IPaymentRepository _paymentRepository;

    private IMapper _mapper;

    public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)

    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    [HttpPost()]
    public IActionResult AddOrder([FromBody] PaymentToUpsert paymentToAdd)
    {
        if (paymentToAdd == null)
            return BadRequest();
        var payment = _mapper.Map<Payment>(paymentToAdd);
        _paymentRepository.Add(payment);
        _paymentRepository.Save();

        return Ok();
    }


}