using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IOrderApiClient
    {
        public Task<string> CreateOrder(CreateOrderRequest orderData);
        public Task AddOrderItems(BasketData basketData, string orderId);
    }
}
