using Microsoft.Extensions.Caching.Memory;

namespace API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly int _requestlimit = 5;
        private readonly TimeSpan _timeWindow = TimeSpan.FromSeconds(10);
        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress))
            {
                await _next(context);
                return;
            }
            var cacheKey = $"RateLimit_{ipAddress}";
            var entry = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _timeWindow;
                return new RateLimit() { count = 0 };
            }) ?? new RateLimit() { count = 0 };

            bool isLimitExceeded = false;
            lock (entry)
            {
                entry.count++;
                if(entry.count >  _requestlimit)
                {
                    isLimitExceeded = true;
                }
            }
            if (isLimitExceeded)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }
            await _next(context);
        }

        
    }
    public class RateLimit
    {
        public int count { get; set; }
    }
}
