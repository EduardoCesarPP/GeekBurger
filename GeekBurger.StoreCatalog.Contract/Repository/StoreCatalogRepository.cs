using AutoMapper;
using GeekBurger.Service.Contract;
using GeekBurger.StoreCatalogs.Contract.Service;
using Newtonsoft.Json;
using GeekBurger.Shared.Model;


namespace GeekBurger.StoreCatalogs.Contract.Repository
{
    public class StoreCatalogRepository : IStoreCatalogRepository
    {
        private IMapper _mapper;
        private IStoreCatalogService _storeCatalogService;
        public StoreCatalogRepository(IStoreCatalogService storeCatalogService, IMapper mapper)
        {
            _storeCatalogService = storeCatalogService;
            _mapper = mapper;
        }

        public async Task<List<Product>> CarregarCatalogo(string storeName, string cpf)
        {
            var products = _mapper.Map<IEnumerable<Product>>(await ConsultarProdutos(storeName));
            var acceptableProducts = new List<Product>(products);

            var restricoes = await ConsultarRestricoes(cpf);

            foreach (Product product in products)
            {
                product.Ingredients = _mapper.Map<List<Ingredient>>(await ConsultarIngredientes(product.ProductId.ToString()));
                if (product.Ingredients.Any(i => i.Allergens.Any(a => restricoes.Restrictions.Contains(a.Characteristic.Name))))
                {
                    acceptableProducts.Remove(product);
                }
            }
            return acceptableProducts;
        }

        public async Task<IEnumerable<IngredientToGet>> ConsultarIngredientes(string idProduto)
        {
            var ingredients = JsonConvert.DeserializeObject<IEnumerable<IngredientToGet>>(await _storeCatalogService.SolicitarDados(idProduto, ServicoExterno.ConsultarIngredientes));
            return ingredients;
        }

        public async Task<IEnumerable<ProductToGet>> ConsultarProdutos(string storeName)
        {
            var produtos = JsonConvert.DeserializeObject<IEnumerable<ProductToGet>>(await _storeCatalogService.SolicitarDados(storeName, ServicoExterno.ConsultarProdutos));
            return produtos;
        }

        public async Task<FoodRestrictionsToGet> ConsultarRestricoes(string cpf)
        {
            var restricoes = JsonConvert.DeserializeObject<FoodRestrictionsToGet>(await _storeCatalogService.SolicitarDados(cpf, ServicoExterno.ConsultarRestricoes));
            return restricoes;
        }
    }
}
