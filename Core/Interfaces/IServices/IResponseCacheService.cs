
namespace Core.Interfaces.IServices
{
    public interface IResponseCacheService
    {
        public Task CachResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        public Task<string?> GetCachedResponseAsync(string cacheKey);
        public Task RemoveCacheByPattern(string pattern);
    }
}
