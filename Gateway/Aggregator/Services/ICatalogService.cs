using Aggregator.Models;

namespace Aggregator.Services
{
    public interface ICatalogService
    {
        public Task<CatalogItem> GetCatalogItemAsync(string id);
    }
}
