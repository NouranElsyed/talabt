using talabat.Core.ServicesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace talabat.Services.Services.Caches
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
    
        }
        public async Task<string?> GetCacheKeyAsync(string key)
        {
            var cacheResponse = await _database.StringGetAsync(key);
            if (cacheResponse.IsNullOrEmpty) return null;
            return cacheResponse;
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expiretime)
        {
            if (response is null) return;
            var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
           await _database.StringSetAsync(key,JsonSerializer.Serialize(response,option),expiretime);
        }
    }
}
