using System;
using System.Collections.Generic;
using Enyim.Caching.Memcached;
using Enyim.Caching.Memcached.Results;

namespace INGA.Framework.Caching.Memcached
{
    interface IMemcachedProvider : IDisposable
    {
        IDictionary<string, object> GetAll(IEnumerable<string> keys);
        T Get<T>(string key);
        object Get(string key);
        IDictionary<string, CasResult<object>> GetWithCas(IEnumerable<string> keys);
        CasResult<T> GetWithCas<T>(string key);
        object GetWithCas(string key);
        bool TryGet(string key, out object obj);
        bool TryGetWithCas(string key, out CasResult<object> obj);
        IDictionary<string, IGetOperationResult> ExecuteGet(IEnumerable<string> keys);
        IGetOperationResult ExecuteGet(string key);
        IGetOperationResult<T> ExecuteGet<T>(string key);
        IGetOperationResult ExecuteTryGet(string key, out object obj);
        bool Store(StoreMode mode, string key, object value);
        bool Store(StoreMode mode, string key, object value, DateTime expireDate);
        bool Store(StoreMode mode, string key, object value, TimeSpan expireTime);
        IStoreOperationResult ExecuteStore(StoreMode mode, string key, object value);
        IStoreOperationResult ExecuteStore(StoreMode mode, string key, object value, DateTime expireDate);
        IStoreOperationResult ExecuteStore(StoreMode mode, string key, object value, TimeSpan expireTime);
        CasResult<bool> StoreCas(StoreMode mode, string key, object value);
        CasResult<bool> StoreCas(StoreMode mode, string key, object value, DateTime expireDate, ulong cas);
        CasResult<bool> StoreCas(StoreMode mode, string key, object value, TimeSpan expireTime, ulong cas);
        CasResult<bool> StoreCas(StoreMode mode, string key, object value, ulong cas);
        bool Remove(string key);
        IRemoveOperationResult ExecuteRemove(string key);
        void Flush();
    }
}
