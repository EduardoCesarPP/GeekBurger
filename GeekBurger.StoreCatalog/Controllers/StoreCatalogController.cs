using AutoMapper;
using GeekBurger.Products.Contract.Repository;
using GeekBurger.Service.Contract;
using Microsoft.AspNetCore.Mvc;

[Route("api/storecatalog")]

public class StoreCatalogController : Controller
{

    private IProductsRepository _productRepository;
    private IUsersRepository _usersRepository;

    private IMapper _mapper;

    public StoreCatalogController(IProductsRepository productRepository, IUsersRepository usersRepository, IMapper mapper)

    {
        _usersRepository = usersRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet()]
    public IActionResult GetProductsByStoreName([FromQuery] string cpf)
    {
        var restrictions = _usersRepository.GetRestrictions(cpf).ToList();
        var products = _productRepository.GetProductsByRestrictions(restrictions).ToList();
        if (products.Count <= 0)
            return NotFound("Nenhum dado encontrado");
        var productsToGet = _mapper.Map<IEnumerable<StoreCatalog>>(products);
        return Ok(productsToGet);
    }


}