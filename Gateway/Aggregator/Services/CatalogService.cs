using Aggregator.Config;
using Aggregator.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public CatalogService(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;
        }

        public async Task<CatalogItem> GetCatalogItemAsync(string id)
        {
            var stringContent = await _httpClient.GetStringAsync(_urls.Catalog + UrlsConfig.CatalogOperations.GetItemById(id));

            return JsonConvert.DeserializeObject<CatalogItem>(stringContent);
        }
    }
}
