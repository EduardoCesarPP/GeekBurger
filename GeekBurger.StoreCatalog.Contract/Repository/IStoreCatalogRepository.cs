using GeekBurger.Shared.Model;
using GeekBurger.Service.Contract;

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