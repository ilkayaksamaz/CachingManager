using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INGA.Framework.Caching.Common
{
    public interface ICachingProvider : IDisposable
    {
        T Set<T>(string key, T value);
        Task<T> SetAsync<T>(string key, T value);
        T Set<T>(string key, T value, DateTime expireDate);
        Task<T> SetAsync<T>(string key, T value, DateTime expireDate);
        T Set<T>(string key, T value, TimeSpan expireTime);
        Task<T> SetAsync<T>(string key, T value, TimeSpan expireTime);
        void Remove(string key);
        T Get<T>(string key);
        T Get<T>(string cacheKey, Func<object> getData);
        T Get<T>(string cacheKey, Func<T> getData);
        T Get<T>(string cacheKey, Func<object> getData, DateTime expireDate);
        T Get<T>(string cacheKey, Func<object> getData, TimeSpan expireTime);
        Task<T> GetAsync<T>(string key);
        Task<T> GetAsync<T>(string cacheKey, Func<object> getData);
        Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> getData);
        Task<T> GetAsync<T>(string cacheKey, Func<T> getData);
        Task<T> GetAsync<T>(string cacheKey, Func<T> getData, DateTime expireDate);
        Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> getData, TimeSpan expireTime);
        Task<object> GetAsync<T>(string cacheKey, Func<object> getData, DateTime expireDate);
        Task<object> GetAsync<T>(string cacheKey, Func<Task<object>> getData, TimeSpan expiration);
        IDictionary<string, object> GetAll(IEnumerable<string> keys);
    }
}
