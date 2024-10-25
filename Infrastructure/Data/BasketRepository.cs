using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(int id)
        {
            return await _database.KeyDeleteAsync(id.ToString());
        }

        public async Task<CustomerBasket> GetBasketAsync(int id)
        {
            var data = await _database.StringGetAsync(id.ToString());
            return (data.IsNullOrEmpty ? null: JsonSerializer.Deserialize<CustomerBasket>(data!))!;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id.ToString(),
                JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if (!created) return null!;
            return await GetBasketAsync(basket.Id);
        }
    }
}
