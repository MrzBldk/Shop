using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IBasketService
    {
        public Task<BasketData> GetByIdAsync(string id);
        public Task UpdateAsync(BasketData currentBasket);
    }
}
