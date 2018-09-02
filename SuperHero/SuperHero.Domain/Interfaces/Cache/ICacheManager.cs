using System;
using System.Threading.Tasks;

namespace SuperHero.Domain.Cache
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(string key, Func<Task<T>> value, int expiration = 1);
        Task UpdateAsync(string key, object value);
        Task RemoveAsync(string key);
    }
}
