using GeekBurger.Products.Contract;
using GeekBurger.Products.Contract.Model;
using GeekBurger.Service.Contract;
using Microsoft.EntityFrameworkCore;
namespace GeekBurger.StoreCatalogs.Contract.Repository
{
    public interface IStoreCatalogRepository
    {
        public Task<IEnumerable<ProductToGet>> ConsultarProdutos(string store);
        public Task<FoodRestrictionsToGet> ConsultarRestricoes(string cpf);
        public Task<IEnumerable<IngredientToGet>> ConsultarIngredientes(string idProduto);
        public Task<List<Product>> CarregarCatalogo(string storeName, string cpf);
    }
}