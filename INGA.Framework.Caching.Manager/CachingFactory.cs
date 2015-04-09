using System.Collections.Specialized;
using System.Linq;
using INGA.Framework.Caching.Common;
using INGA.Framework.Caching.InMemory;
using INGA.Framework.Caching.Memcached;
using INGA.Framework.Caching.Redis;
using INGA.Framework.Helpers.Configuration;
using INGA.Framework.Helpers.Configuration.ConfigurationSections;

namespace INGA.Framework.Caching.Manager
{
    public abstract class CachingFactoryBase<T> where T : new()
    {
        public virtual ICachingProvider CreateInstance()
        {
            T thing = new T();
            return thing as ICachingProvider;
        }
    }

    public class RedisFactory : CachingFactoryBase<RedisCacheProvider>
    {  
      
        private readonly RedisConnectionOptions _connectionOptions;

        public RedisFactory(string host, int port, string username = "", string password = "")
        {
            if (_connectionOptions == null)
            {
                _connectionOptions = new RedisConnectionOptions()
                {
                    Host = host,
                    Port = port,
                    Username = username,
                    Password = password
                };
            }
        }

        public override ICachingProvider CreateInstance()
        {
            RedisCacheProvider redisCacheProvider = new RedisCacheProvider(_connectionOptions);
            return redisCacheProvider;
        }

      
    }


    public class MemcachedFactory : CachingFactoryBase<MemcachedCacheProvider>
    {
        private readonly MemcachedConnectionOptions _connectionOptions;

        public MemcachedFactory(string host, int port, string username = "", string password = "")
        {
            if (_connectionOptions == null)
            {
                _connectionOptions = new MemcachedConnectionOptions()
                {
                    Host = host,
                    Port = port,
                    Username = username,
                    Password = password
                };
            }
        }

        public override ICachingProvider CreateInstance()
        {
            MemcachedCacheProvider memcachedCacheProvider = new MemcachedCacheProvider(_connectionOptions);
            return memcachedCacheProvider;
        }
    }

    public class InMemoryFactory : CachingFactoryBase<InMemoryCacheProvider>
    {
        private readonly InMemoryConnectionOptions _connectionOptions;

        public InMemoryFactory(string name, NameValueCollection config=null)
        {
            if (_connectionOptions == null)
            {
                _connectionOptions = new InMemoryConnectionOptions()
                {
                    Name = name,
                    Config = config
                };
            }
        }
        public override ICachingProvider CreateInstance()
        {
            InMemoryCacheProvider inMemoryCacheProvider = new InMemoryCacheProvider(_connectionOptions);
            return inMemoryCacheProvider;
        }
    }

    public static class CachingFactory
    {
        
        private static ICachingProvider _instance;

        static readonly CacheProvidersSection Options =
             ConfigurationHelper.GetSection<CacheProvidersSection>("CacheProvidersSection");
        public static ICachingProvider Instance
        {
            get
            {
                var collectionItem = Options.CacheProviderCollection.FirstOrDefault(c => c.IsActive == "1");
                if (collectionItem == null) return null;
                switch (collectionItem.Name)
                {
                    case "Redis":
                        _instance = new RedisFactory(collectionItem.Host, collectionItem.Port, collectionItem.Username, collectionItem.Password).CreateInstance();
                        return _instance;
                    case "Memcached":
                        _instance = new MemcachedFactory(collectionItem.Host, collectionItem.Port, collectionItem.Username, collectionItem.Password).CreateInstance();
                        return _instance;
                    case "InMemory":
                        _instance = new InMemoryFactory(collectionItem.CacheName).CreateInstance();
                        return _instance;
                    default:
                        _instance = new MemcachedFactory(collectionItem.Host, collectionItem.Port, collectionItem.Username, collectionItem.Password).CreateInstance();
                        return _instance;
                }
            }
        }
    }

   
}

