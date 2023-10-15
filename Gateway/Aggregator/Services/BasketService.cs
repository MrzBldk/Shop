using Aggregator.Config;
using Aggregator.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _apiClient;
        private readonly UrlsConfig _urls;

        public BasketService(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _urls = config.Value;
        }

        public async Task<BasketData> GetByIdAsync(string id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Basket + UrlsConfig.BasketOperations.GetById(id));
            var basket = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<BasketData>(data) : null;

            return basket;
        }

        public async Task UpdateAsync(BasketData currentBasket)
        {
            var basketContent = new StringContent(JsonConvert.SerializeObject(currentBasket), System.Text.Encoding.UTF8, "application/json");

            await _apiClient.PostAsync(_urls.Basket + UrlsConfig.BasketOperations.UpdateBasket(), basketContent);
        }

        public async Task DeleteAsync(string id)
        {
            await _apiClient.DeleteAsync(_urls.Basket + UrlsConfig.BasketOperations.DeleteById(id));
        }
    }
}
