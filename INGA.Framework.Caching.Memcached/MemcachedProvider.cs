using System;
using System.Collections.Generic;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Enyim.Caching.Memcached.Results;

namespace INGA.Framework.Caching.Memcached
{
    public class MemcachedProvider : IMemcachedProvider
    {
        private static MemcachedClient _mc;
        public MemcachedProvider(MemcachedClient client)
        {
            if (_mc == null)
            {
                _mc = client;
            }
        }

        #region Implementation of IMemcacheProvider

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            return _mc.Get(keys);
        }

        public T Get<T>(string key)
        {
            return _mc.Get<T>(key);
        }

        public object Get(string key)
        {
            return _mc.Get(key);
        }

        public IDictionary<string, CasResult<object>> GetWithCas(IEnumerable<string> keys)
        {
            return _mc.GetWithCas(keys);
        }

        public CasResult<T> GetWithCas<T>(string key)
        {
            return _mc.GetWithCas<T>(key);
        }

        public object GetWithCas(string key)
        {
            return _mc.GetWithCas(key);
        }

        public bool TryGet(string key, out object obj)
        {
            return _mc.TryGet(key, out obj);
        }

        public bool TryGetWithCas(string key, out CasResult<object> obj)
        {
            return _mc.TryGetWithCas(key, out obj);
        }

        public IDictionary<string, IGetOperationResult> ExecuteGet(IEnumerable<string> keys)
        {
            return _mc.ExecuteGet(keys);
        }

        public IGetOperationResult ExecuteGet(string key)
        {
            return _mc.ExecuteGet(key);
        }

        public IGetOperationResult<T> ExecuteGet<T>(string key)
        {
            return _mc.ExecuteGet<T>(key);
        }

        public IGetOperationResult ExecuteTryGet(string key, out object obj)
        {
            return _mc.ExecuteTryGet(key, out obj);
        }

        public bool Store(StoreMode mode, string key, object value)
        {
            return _mc.Store(mode, key, value);
        }

        public bool Store(StoreMode mode, string key, object value, DateTime expireDate)
        {
            return _mc.Store(mode, key, value, expireDate);
        }

        public bool Store(StoreMode mode, string key, object value, TimeSpan expireTime)
        {
            return _mc.Store(mode, key, value, expireTime);
        }

        public IStoreOperationResult ExecuteStore(StoreMode mode, string key, object value)
        {
            return _mc.ExecuteStore(mode, key, value);
        }

        public IStoreOperationResult ExecuteStore(StoreMode mode, string key, object value, DateTime expireDate)
        {
            return _mc.ExecuteStore(mode, key, value, expireDate);
        }

        public IStoreOperationResult ExecuteStore(StoreMode mode, string key, object value, TimeSpan expireTime)
        {
            return _mc.ExecuteStore(mode, key, value, expireTime);
        }

        public CasResult<bool> StoreCas(StoreMode mode, string key, object value)
        {
            return _mc.Cas(mode, key, value);
        }

        public CasResult<bool> StoreCas(StoreMode mode, string key, object value, DateTime expireDate, ulong cas)
        {
            return _mc.Cas(mode, key, value, expireDate, cas);
        }

        public CasResult<bool> StoreCas(StoreMode mode, string key, object value, TimeSpan expireTime, ulong cas)
        {
            return _mc.Cas(mode, key, value, expireTime, cas);
        }

        public CasResult<bool> StoreCas(StoreMode mode, string key, object value, ulong cas)
        {
            return _mc.Cas(mode, key, value, cas);
        }

        public bool Remove(string key)
        {
            return _mc.Remove(key);
        }

        public IRemoveOperationResult ExecuteRemove(string key)
        {
            return
             _mc.ExecuteRemove(key);
        }

        public void Flush()
        {
            _mc.FlushAll();
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _mc.Dispose();
        }

        #endregion
    }
}
