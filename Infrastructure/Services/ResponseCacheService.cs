
using Core.Interfaces.IServices;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ResponseCacheService(IConnectionMultiplexer redis) : IResponseCacheService
    {
        private readonly IDatabase _database = redis.GetDatabase(1);

        public async Task CachResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serialzedResponse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(cacheKey, serialzedResponse, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string cacheKey)
        {
            var CachedResponse = await _database.StringGetAsync(cacheKey);
            if (CachedResponse.IsNullOrEmpty) return null;
            return CachedResponse;
        }

        public async Task RemoveCacheByPattern(string pattern)
        {
            var server = redis.GetServer(redis.GetEndPoints().First());
            var keys = server.Keys(database: 1, pattern: $"*{pattern}*").ToArray();

            if (keys.Length != 0)
            {
                await _database.KeyDeleteAsync(keys);
            }
        }
    }
}
