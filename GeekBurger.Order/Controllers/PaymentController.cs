using AutoMapper;

using GeekBurger.Service.Contract;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]

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
    public IActionResult Pay([FromBody] PaymentToUpsert paymentToAdd)
    {
        if (paymentToAdd == null)
            return BadRequest();
        var payment = _mapper.Map<Payment>(paymentToAdd);
        _paymentRepository.Register(payment, paymentToAdd.StoreName);
        return Ok();
    }


}