using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Projects.Services;

public interface ICacheService
{
    /// <summary>
    /// Get cached data by key, if cache miss, fetch data and cache it
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="fetchValue">Fetched data which will be cached</param>
    /// <param name="expiry">Expired after time</param>
    /// <returns>Cache value if cache hint or fetched value</returns>
    Task<T?> GetOrSetCacheAsync<T>(string key, Func<Task<T>> fetchValue, int expiry = 30);

    /// <summary>
    /// Get cached data by key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <returns>Cached data if cache hint or null</returns>
    Task<string?> GetStringAsync(string key);

    /// <summary>
    /// Set cache data by key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="data">Cache data</param>
    /// <param name="expiry">Expired after time</param>
    /// <returns></returns>
    Task SetStringAsync(string key, string data, int expiry = 30);

    /// <summary>
    /// Remove cache data by key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <returns></returns>
    Task InvalidateCacheAsync(string key);
}

public class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetOrSetCacheAsync<T>(string key, Func<Task<T>> fetchValue, int expiry = 30)
    {
        var cachedValue = await cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedValue))
        {
            var data = JsonConvert.DeserializeObject<T>(cachedValue);
            return data;
        }

        var value = await fetchValue();
        await cache.SetStringAsync(key, JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(expiry)
        });

        return value;
    }

    public async Task<string?> GetStringAsync(string key)
    {
        var cached = await cache.GetStringAsync(key);

        return cached;
    }

    public async Task SetStringAsync(string key, string data, int expiry = 30)
    {
        await cache.SetStringAsync(key, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiry)
        });
    }

    public async Task InvalidateCacheAsync(string key)
    {
        await cache.RemoveAsync(key);
    }
}
