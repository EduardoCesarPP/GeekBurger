using Microsoft.Extensions.Hosting;

namespace GeekBurger.StoreCatalogs.Contract.Service
{
    public interface IStoreCatalogService : IHostedService
    {
        public Task<string> SolicitarDados(string key, ServicoExterno servicoExterno);
    }
}