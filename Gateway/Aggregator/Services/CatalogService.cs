using Aggregator.Config;
using Aggregator.Models;
using Newtonsoft.Json;

namespace Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public CatalogService(HttpClient httpClient, UrlsConfig urls)
        {
            _httpClient = httpClient;
            _urls = urls;
        }

        public async Task<CatalogItem> GetCatalogItemAsync(string id)
        {
            var stringContent = await _httpClient.GetStringAsync(_urls.Catalog + UrlsConfig.CatalogOperations.GetItemById(id));

            return JsonConvert.DeserializeObject<CatalogItem>(stringContent);
        }
    }
}
