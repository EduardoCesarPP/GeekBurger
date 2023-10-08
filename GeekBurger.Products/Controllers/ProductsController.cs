using AutoMapper;
using GeekBurger.Products.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Products.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository
                ?? throw new ArgumentNullException(nameof(productsRepository));
        }

        [HttpGet("{storeName}")]
        public IActionResult GetProductsByStoreName(string storeName)
        {
            var productsByStores = _productsRepository.GetProductsbyStoreName(storeName).ToList();

            if (productsByStores.Count <= 0)
                return NotFound();

            return Ok(productsByStores);
        }
    }
}
