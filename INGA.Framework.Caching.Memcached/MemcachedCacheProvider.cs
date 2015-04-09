using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using INGA.Framework.Caching.Common;

namespace INGA.Framework.Caching.Memcached
{
    public class MemcachedCacheProvider : ICachingProvider
    {
        private readonly MemcachedProvider _provider;
        private readonly MemcachedClient _client;
        public MemcachedCacheProvider(MemcachedConnectionOptions connectionOptions)
        {
            if (_client == null)
            {
                _client = Initialize(connectionOptions);
                _provider = new MemcachedProvider(_client);
            }

        }

        public MemcachedCacheProvider()
        {
            if (_client == null)
            {
                _client = Initialize(new MemcachedConnectionOptions() { Host = "127.0.0.1", Port = 11211 });
                _provider = new MemcachedProvider(_client);
            }
        }

        private MemcachedClient Initialize(MemcachedConnectionOptions connectionOptions)
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Parse(connectionOptions.Host), connectionOptions.Port));
            config.Protocol = MemcachedProtocol.Binary;
            //config.Authentication.Type = typeof(PlainTextAuthenticator);
            //config.Authentication.Parameters["userName"] = username;
            //config.Authentication.Parameters["password"] = password;
            return new MemcachedClient(config);
        }
        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _provider.Dispose();
        }

        #endregion

        #region Implementation of ICachingProvider

        public T Set<T>(string key, T value)
        {
            return _provider.Store(StoreMode.Set, key, value) ? value : value;
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            return await Task.Run(() => _provider.Store(StoreMode.Set, key, value) ? value : value);
   
        }

        public T Set<T>(string key, T value, DateTime expireDate)
        {
            return _provider.Store(StoreMode.Set, key, value, expireDate) ? value : value;
        }

        public async Task<T> SetAsync<T>(string key, T value, DateTime expireDate)
        {
            return await Task.Run(() => _provider.Store(StoreMode.Set, key, value, expireDate) ? value : value);
        }

        public T Set<T>(string key, T value, TimeSpan expireTime)
        {
            return _provider.Store(StoreMode.Set, key, value, expireTime) ? value : value;
        }

        public async Task<T> SetAsync<T>(string key, T value, TimeSpan expireTime)
        {
            return await Task.Run(() => _provider.Store(StoreMode.Set, key, value, expireTime) ? value : value);
        }

        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        public T Get<T>(string key)
        {
            return _provider.Get<T>(key);
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
