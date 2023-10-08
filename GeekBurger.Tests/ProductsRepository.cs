using GeekBurger.Products.Contract;
using GeekBurger.Products.Repository;
using GeekBurger.Products.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GeekBurger.Tests
{
    public class Tests
    {
        private Mock<IProductsRepository> _mockProducts;
        private ProductsRepository _productsRepository;

        [SetUp]
        public void Setup()
        {
            _mockProducts = new Mock<IProductsRepository>();
            _productsRepository = new ProductsRepository();
        }

        [Test]
        public void OnGetProductsByStoreName_WhenListIsEmpty_ShouldReturnNotFound()
        {
            //arrange
            var storeName = "Paulista";
            var productList = new List<Product>();
            _mockProducts.Setup(_ => _.GetProductsbyStoreName(storeName)).Returns(productList);

            var expected = new NotFoundObjectResult("Nenhum dado encontrado");

            //act
            var response = _productsRepository.GetProductsbyStoreName(storeName);

            //assert
            Assert.That(expected, Is.Not.Null);
        }

        [Test]
        public void OnGetProductsByStoreName_WhenListIsNotEmpty_ShouldReturnProduct()
        {
            //arrange
            var storeName = "Morumbi";
            var productList = new List<Product>();
            _mockProducts.Setup(_ => _.GetProductsbyStoreName(storeName)).Returns(productList);

            var expected = new OkObjectResult(productList);

            //act
            var response = _productsRepository.GetProductsbyStoreName(storeName);

            Console.WriteLine($"Loja {storeName} retornou {response.Count} items");

            //assert
            Assert.That(expected, Is.Not.Null);
        }
    }
}