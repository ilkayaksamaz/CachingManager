using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace INGA.Framework.Caching.InMemory
{
    public interface IInMemoryCacheProvider 
    {
        void Store(CacheItem item, CacheItemPolicy policy = null);
        void Store(string key, object value, CacheItemPolicy policy = null, string regionName = "");
        void Store(string key, object value, DateTimeOffset expireTime, string regionName = "");

        void Store<T>(string cacheKey, Func<T> getData, CacheItemPolicy policy = null);

        bool Add(CacheItem item, CacheItemPolicy policy = null);
        bool Add(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null);
        bool Add(string key, object value, CacheItemPolicy policy, string regionName = null);

        object Get(string key, string regionName = null);
        CacheItem GetCacheItem(string key, string regionName = null);

        IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null);
        IDictionary<string, object> GetValues(string regionName, params string[] keys);

        object Remove(string key, string regionName = null);


    }
}
