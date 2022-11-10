using Basket.DAL.Entities;
using StackExchange.Redis;
using System.Net;
using System.Text.Json;

namespace Basket.DAL.Repositories
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            RedisValue data = await _database.StringGetAsync(customerId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            var basket = JsonSerializer.Deserialize<CustomerBasket>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return basket;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            bool created = await _database.StringSetAsync(basket.BuyerId, JsonSerializer.Serialize(basket));

            if (!created)
            {
                return null;
            }

            return await GetBasketAsync(basket.BuyerId);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public IEnumerable<string> GetUsers()
        {
            IServer server = GetServer();
            IEnumerable<RedisKey> data = server.Keys();

            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            EndPoint[] endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

    }
}
