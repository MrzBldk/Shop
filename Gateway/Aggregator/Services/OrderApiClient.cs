using Aggregator.Config;
using Aggregator.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aggregator.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;

        public OrderApiClient(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;
        }

        public async Task<string> CreateOrder(CreateOrderRequest orderData)
        {
            string url = _urls.Ordering + UrlsConfig.OrderingOperations.CreateOrder();
            StringContent content = new(JsonConvert.SerializeObject(orderData), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responce = await _httpClient.PostAsync(url, content);
            responce.EnsureSuccessStatusCode();

            return (await responce.Content.ReadFromJsonAsync<Guid>()).ToString();
        }

        public async Task AddOrderItems(BasketData basketData, string orderId)
        {
            string url = _urls.Ordering + UrlsConfig.OrderingOperations.AddMultiple(orderId);
            List<OrderItem> orderItems = new();
            foreach(BasketDataItem item in basketData.Items)
            {
                orderItems.Add(new() 
                { 
                    Name = item.ProductName, 
                    ProductId = Guid.Parse(item.Id), 
                    UnitPrice = item.UnitPrice, 
                    Units = item.Quantity
                });
            }
            StringContent content = new(JsonConvert.SerializeObject(orderItems), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responce = await _httpClient.PostAsync(url, content);
            responce.EnsureSuccessStatusCode();
        }
    }
}
