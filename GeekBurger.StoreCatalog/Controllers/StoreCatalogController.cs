using AutoMapper;

using GeekBurger.Service.Contract;
using GeekBurger.StoreCatalogs.Contract.Repository;
using Microsoft.AspNetCore.Mvc;

[Route("api/storecatalog")]

public class StoreCatalogController : Controller
{

    private IStoreCatalogRepository _storeCatalogRepository;

    private IMapper _mapper;

    public StoreCatalogController(IMapper mapper, IStoreCatalogRepository storeCatalogRepository)
    {
        _mapper = mapper;
        _storeCatalogRepository = storeCatalogRepository;
    }

    [HttpGet()]
    public async Task<IActionResult> GetStoreCatalog([FromQuery] string storeName, [FromQuery] string cpf)
    {
        var acceptableProducts = _mapper.Map<List<ProductToGet>>(await _storeCatalogRepository.CarregarCatalogo(storeName, cpf));
        return Ok(acceptableProducts);
    }


    [HttpPost()]
    public async Task<IActionResult> Teste([FromBody] OrderToUpsert storeName)
    {
        return Ok();
    }


}