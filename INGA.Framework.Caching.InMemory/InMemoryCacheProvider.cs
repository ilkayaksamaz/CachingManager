using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Caching;
using System.Threading.Tasks;
using INGA.Framework.Caching.Common;

namespace INGA.Framework.Caching.InMemory
{
    public class InMemoryCacheProvider : ICachingProvider
    {
        private readonly InMemoryProvider _provider;

        public InMemoryCacheProvider(InMemoryConnectionOptions connectionOptions)
        {
            if (_provider == null)
            {
                _provider = new InMemoryProvider(connectionOptions.Name, connectionOptions.Config);
            }
        }
        public InMemoryCacheProvider(string name, NameValueCollection config = null)
        {
            if (_provider == null)
            {
                _provider = new InMemoryProvider(name, config);
            }
        }

        public InMemoryCacheProvider()
        {
            if (_provider == null)
            {
                _provider = new InMemoryProvider("CachingProvider");
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
           //never use.
        }

        #endregion

        #region Implementation of ICachingProvider

        public T Set<T>(string key, T value)
        {
            _provider.Store(new CacheItem(key, value));
            return value;
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            return await Task.Run(() =>
            {
                _provider.Store(new CacheItem(key, value)); return value;
            });
        }

        public T Set<T>(string key, T value, DateTime expireDate)
        {
            _provider.Store(key,value,expireDate);
            return value;
        }

        public async Task<T> SetAsync<T>(string key, T value, DateTime expireDate)
        {
            return await Task.Run(() =>
            {
                _provider.Store(key, value, expireDate); return value;
            });
        }

        public T Set<T>(string key, T value, TimeSpan expireTime)
        {
            System.DateTime today = System.DateTime.Now;
            _provider.Store(key, value, today.Add(expireTime));
            return value;
        }

        public async Task<T> SetAsync<T>(string key, T value, TimeSpan expireTime)
        {
            return await Task.Run(() =>
            {
                System.DateTime today = System.DateTime.Now;
                _provider.Store(key, value, today.Add(expireTime));
                return value;
            });
        }

        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        public T Get<T>(string key)
        {
            return (T)_provider.Get(key);
        }

        public T Get<T>(string cacheKey, Func<object> getData)
        {
            T data = (T)_provider.Get(cacheKey); 
            data =(T)getData();
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
            //expireDate is not usable
            T data = (T)_provider.Get(cacheKey);
            data = (T)getData();
            return data;
        }

        public T Get<T>(string cacheKey, Func<object> getData, TimeSpan expireTime)
        {
            //expireDate is not usable
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

        public async Task<T> GetAsync<T>(string cacheKey, Func<T> getData)
        {
            //expireDate is not usable
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> getData)
        {
            //expireDate is not usable
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<T> getData, DateTime expireDate)
        {
            //expireDate is not usable
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> getData, TimeSpan expireTime)
        {
            //expireDate is not usable
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<object> GetAsync<T>(string cacheKey, Func<object> getData, DateTime expireDate)
        {
            //expireDate is not usable
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public async Task<object> GetAsync<T>(string cacheKey, Func<Task<object>> getData, TimeSpan expiration)
        {
            //expireDate is not usable
            return await Task.Run(() => Get<T>(cacheKey, getData));
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            return _provider.GetValues(keys);
        }

        #endregion
    }


    
}
