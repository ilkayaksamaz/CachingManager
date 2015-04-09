using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Caching;

namespace INGA.Framework.Caching.InMemory
{
    public class InMemoryProvider : IInMemoryCacheProvider
    {
        private readonly ObjectCache _cacheObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Runtime.Caching.MemoryCache"/> class. 
        /// </summary>
        /// <param name="name">The name to use to look up configuration information. <paramref name="Note"/>   It is not required for configuration information to exist for every name.If a matching configuration entry exists, the configuration information is used to configure the <see cref="T:System.Runtime.Caching.MemoryCache"/> instance. If a matching configuration entry does not exist, the name can be accessed through the <see cref="P:System.Runtime.Caching.MemoryCache.Name"/> property, because the specified name is associated with the <see cref="T:System.Runtime.Caching.MemoryCache"/> instance. For information about memory cache configuration, see <see cref="T:System.Runtime.Caching.Configuration.MemoryCacheElement"/>.</param><param name="config">A collection of name/value pairs of configuration information to use for configuring the cache. </param><exception cref="T:System.ArgumentNullException"><paramref name="name"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="name"/> is an empty string. </exception><exception cref="T:System.ArgumentException">The string value "default" (case insensitive) is assigned to <paramref name="name"/>. The value "default" cannot be assigned to a new <see cref="T:System.Runtime.Caching.MemoryCache"/> instance, because the value is reserved for use by the <see cref="P:System.Runtime.Caching.MemoryCache.Default"/> property.</exception><exception cref="T:System.Configuration.ConfigurationException">A value in the <paramref name="config"/> collection is invalid. </exception><exception cref="T:System.ArgumentException">A name or value in the <paramref name="config"/> parameter could not be parsed.</exception>
        public InMemoryProvider(string name, NameValueCollection config = null)
        {
            _cacheObject = new MemoryCache(name, config);
        }
        #region Implementation of IInMemoryCacheProvider

        public void Store(CacheItem item, CacheItemPolicy policy = null)
        {
            _cacheObject.Set(item, policy);
        }

        public void Store(string key, object value, CacheItemPolicy policy = null, string regionName = "")
        {
            _cacheObject.Set(key, value, policy, regionName);
        }

        public void Store(string key, object value, DateTimeOffset expireTime, string regionName = "")
        {
            _cacheObject.Set(key, value, expireTime, regionName);
        }

        public void Store<T>(string cacheKey, Func<T> getData, CacheItemPolicy policy = null)
        {
            _cacheObject.Set(cacheKey, getData(), policy);
        }

        public bool Add(CacheItem item, CacheItemPolicy policy = null)
        {
           return  _cacheObject.Add(item, policy);
        }

        public bool Add(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            return _cacheObject.Add(key, value, absoluteExpiration, regionName);
        }

        public bool Add(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            return _cacheObject.Add(key, value, policy, regionName);
        }

        public object Get(string key, string regionName = null)
        {
            return _cacheObject.Get(key, regionName);
        }

        public CacheItem GetCacheItem(string key, string regionName = null)
        {
            return _cacheObject.GetCacheItem(key, regionName);
        }

        public IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null)
        {
            return _cacheObject.GetValues(keys, regionName);
        }

        public IDictionary<string, object> GetValues(string regionName, params string[] keys)
        {
            return _cacheObject.GetValues(regionName, keys);
        }

        public object Remove(string key, string regionName = null)
        {
            return _cacheObject.Remove(key, regionName);
        }

       
       
        #endregion

       
    }


    
}
