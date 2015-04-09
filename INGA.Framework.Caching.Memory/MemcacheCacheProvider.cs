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

namespace INGA.Framework.Caching.Memory
{
    public class MemcacheCacheProvider : ICachingProvider
    {
        private MemcacheProvider _provider;
        private readonly MemcachedClient _client;
        public MemcacheCacheProvider(MemcacheConnectionOptions connectionOptions)
        {
            if (_client == null)
            {
                _client = Initialize(connectionOptions);
                _provider = new MemcacheProvider(_client);
            }
            
        }
        private MemcachedClient Initialize(MemcacheConnectionOptions connectionOptions)
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

        public T Set<T>(string key, T value, DateTime expireDate)
        {
            return _provider.Store(StoreMode.Set, key, value, expireDate) ? value : value;
        }

        public T Set<T>(string key, T value, TimeSpan expireTime)
        {
            return _provider.Store(StoreMode.Set, key, value, expireTime) ? value : value;
        }

        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        public T Get<T>(string key)
        {
            return _provider.Get<T>(key);
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            return _provider.GetAll(keys);
        }

        #endregion
    }
}
