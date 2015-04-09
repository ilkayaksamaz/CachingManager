using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INGA.Framework.Caching.Common;
using ServiceStack.Redis;

namespace INGA.Framework.Caching.Redis
{
    public class RedisCacheProvider : ICachingProvider
    {
        private readonly RedisClient _client;
        private readonly RedisProvider _provider;

        public RedisCacheProvider(IConnectionOptions connectionOptions)
        {
            _client =
                Initialize(new RedisConnectionOptions()
                {
                    Host = connectionOptions.Host,
                    Port = connectionOptions.Port,
                    Username = connectionOptions.Username,
                    Password = connectionOptions.Password
                });
            _provider = new RedisProvider(_client);
        }
        public RedisCacheProvider(RedisConnectionOptions configOptions)
        {
            _client = Initialize(configOptions);
            _provider = new RedisProvider(_client);
        }

        public RedisCacheProvider()
        {
            _client = Initialize(new RedisConnectionOptions() {Host = "127.0.0.1", Port = 6379});
            _provider = new RedisProvider(_client);
        }

        private RedisClient Initialize(RedisConnectionOptions configOptions)
        {
            if (_client == null)
            {
                if (configOptions.EndPoint != null)
                {
                    return new RedisClient(configOptions.EndPoint);
                }
                else if (!string.IsNullOrEmpty(configOptions.Host))
                {
                    return new RedisClient(configOptions.Host);
                }
                else if (!string.IsNullOrEmpty(configOptions.Host) && configOptions.Port > 0)
                {
                    return new RedisClient(configOptions.Host, configOptions.Port);
                }
                else if (configOptions.Uri != null)
                {
                    return new RedisClient(configOptions.Uri);
                }
                else if (!string.IsNullOrEmpty(configOptions.Host) && configOptions.Port > 0 &&
                         !string.IsNullOrEmpty(configOptions.Password) && configOptions.DbLocation > 0)
                {
                    return new RedisClient(configOptions.Host, configOptions.Port, configOptions.Password,
                        configOptions.DbLocation);
                }
            }
            return new RedisClient(configOptions.Uri);
        }

        #region Implementation of IDisposable
        public void Dispose()
        {
            _provider.Dispose();
        }

        #endregion

        #region Implementation of ICachingProvider

        public T Set<T>(string key, T value)
        {
            return _provider.Set(key, value) ? value : value;
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            return await Task.Run(() => _provider.Set(key, value) ? value : value);
        }

        public T Set<T>(string key, T value, DateTime expireDate)
        {
            return _provider.Set(key, value, expireDate) ? value : value;
        }

        public async Task<T> SetAsync<T>(string key, T value, DateTime expireDate)
        {
            return await Task.Run(() => _provider.Set(key, value, expireDate) ? value : value);
        }

        public T Set<T>(string key, T value, TimeSpan expireTime)
        {
            return _provider.Set(key, value, expireTime) ? value : value;
        }

        public async Task<T> SetAsync<T>(string key, T value, TimeSpan expireTime)
        {
            return await Task.Run(() => _provider.Set(key, value, expireTime) ? value : value);
        }

        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        public T Get<T>(string key)
        {
            return (T) _provider.Get(key);
        }

        public T Get<T>(string cacheKey, Func<object> getData)
        {
            T data = (T)_provider.Get(cacheKey);
            data = (T)getData();
            return data;
        }

        public T Get<T>(string cacheKey, Func<T> getData)
        {
            T data = (T)_provider.Get(cacheKey);
            data = (T)getData();
            return data;
        }

        public T Get<T>(string cacheKey, Func<object> getData, DateTime expireDate)
        {
            T data = (T)_provider.Get(cacheKey);
            data = (T)getData();
            return data;
        }

        public T Get<T>(string cacheKey, Func<object> getData, TimeSpan expireTime)
        {
            T data = (T)_provider.Get(cacheKey);
            data = (T)getData();
            return data;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() => (T)_provider.Get(key));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<object> getData)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> getData)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<T> getData)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<T> getData, DateTime expireDate)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> getData, TimeSpan expireTime)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<object> GetAsync<T>(string cacheKey, Func<object> getData, DateTime expireDate)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<object> GetAsync<T>(string cacheKey, Func<Task<object>> getData, TimeSpan expiration)
        {
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
           return _provider.GetAll(keys);
        }

        #endregion
    }
}
