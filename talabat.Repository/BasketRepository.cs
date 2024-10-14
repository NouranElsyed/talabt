using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;

namespace talabat.Repository
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis) // ask clr to create obj from class implement interface
        {
           _database =  redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
          
            return  basket.IsNull?null:JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var CreateOrUpdate = await _database.StringSetAsync(basket.Id, JsonBasket, TimeSpan.FromDays(1));
            if (!CreateOrUpdate) return null;
          return await GetBasketAsync(basket.Id);  
        }

    }
}
