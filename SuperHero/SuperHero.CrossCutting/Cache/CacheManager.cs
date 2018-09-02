using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SuperHero.Domain.Cache;

namespace SuperHero.CrossCutting.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;

        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> value, int expiration = 1)
        {
            string cache = await _cache.GetStringAsync(key);

            if (cache == null)
            {
                var options = new DistributedCacheEntryOptions();
                options.SetSlidingExpiration(TimeSpan.FromMinutes(expiration));

                var valueResult = await value();
                cache = JsonConvert.SerializeObject(valueResult);
                await _cache.SetStringAsync(key, cache, options);
            }

            var result = JsonConvert.DeserializeObject<T>(cache);

            return result;
        }

        public async Task UpdateAsync(string key, object value)
        {
            var cache = await _cache.GetStringAsync(key);

            if (cache != null)
            {
                await _cache.RemoveAsync(key);

                var options = new DistributedCacheEntryOptions();
                options.SetSlidingExpiration(TimeSpan.FromMinutes(1));

                var newCache = JsonConvert.SerializeObject(value);
                await _cache.SetStringAsync(key, newCache, options);
            }
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
