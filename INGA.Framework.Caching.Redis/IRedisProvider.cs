using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace INGA.Framework.Caching.Redis
{
    public interface IRedisProvider 
     : IDisposable
    {
        //store operations
        object Store(object entity);
        void StoreAll(IEnumerable<object> entities);
        void StoreAsHash(object entity);
        void StoreDifferencesFromSet(IRedisSet<object> intoSet, IRedisSet<object> fromSet, params IRedisSet<object>[] withSets);
        void StoreIntersectFromSets(IRedisSet<object> intoSet, params IRedisSet<object>[] sets);
        long StoreIntersectFromSortedSets(IRedisSortedSet<object> intoSetId, params IRedisSortedSet<object>[] setIds);
        object StoreObject(object obj);
        void StoreUnionFromSets(IRedisSet<object> intoSet, params IRedisSet<object>[] sets);
        long StoreUnionFromSortedSets(IRedisSortedSet<object> intoSetId, params IRedisSortedSet<object>[] setIds);

        //set operations use with spesific cache key etc.
        bool Set(string key, object entity);
        bool Set(string key, object entity, DateTime expireDate);
        bool Set(string key, object entity, TimeSpan expireTime);
        bool Set(string key, byte[] values);
        bool Set(string key, byte[] values, bool exists, int existingSecond, long existingMillisecond);
        void Set(string key, byte[] values, int existingSecond, long existingMillisecond);
        void Set<T>(T item, string hash, string value, string keyName);
        void Set<T>(T item, List<string> hash, List<string> value, string keyName);
        void SetAll<T>(List<T> list, string hash, string value, string keyName);
        void SetAll<T>(List<T> list, List<string> hash, List<string> value, string keyName);
        void SetAll(Dictionary<string, string> values);
        void SetAll(IEnumerable<string> firstValues, IEnumerable<string> secondValues);
        long SetBit(string key, int offset, int value);
        void SetEx(string key, int expireInSeconds, byte[] value);
        long SetNx(string key, byte[] value);
        long SetRange(string key, int offset, byte[] value);

        //set entry operations
        void SetEntry(string key, string value);
        void SetEntry(string key, string value, TimeSpan expireIn);
        bool SetEntryIfExists(string key, string value);
        bool SetEntryIfExists(string key, string value, TimeSpan expireIn);
        bool SetEntryIfNotExists(string key, string value);
        bool SetEntryIfNotExists(string key, string value, TimeSpan expireIn);
        bool SetEntryInHash(string hashId, string key, string value);
        bool SetEntryInHashIfNotExists(string hashId, string key, string value);
        void SetItemInList(string listId, int listIndex, string value);
        void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);
        bool SortedSetContainsItem(string setId, string value);

        //add operations
        bool Add(string key, object value);
        bool Add(string key, object value, DateTime expiresAt);
        bool Add(string key, object value, TimeSpan expiresIn);
        void AddItemToList(string listId, string value);
        void AddItemToSet(string setId, string item);
        bool AddItemToSortedSet(string setId, string value);
        bool AddItemToSortedSet(string setId, string value, double score);
        bool AddItemToSortedSet(string setId, string value, long score);
        void AddRangeToList(string listId, List<string> values);
        void AddRangeToSet(string setId, List<string> items);
        bool AddRangeToSortedSet(string setId, List<string> values, double score);
        bool AddRangeToSortedSet(string setId, List<string> values, long score);

        //delete 
        void Delete(object entity);
        void DeleteAll();
        void DeleteById(object id);
        void DeleteByIds(ICollection ids);
        bool Remove(string key);
        void RemoveAll(IEnumerable<string> keys);
        void RemoveAllFromList(string listId);
        string RemoveEndFromList(string listId);
        bool RemoveEntry(params string[] keys);
        bool RemoveEntryFromHash(string hashId, string key);
        long RemoveItemFromList(string listId, string value);
        long RemoveItemFromList(string listId, string value, int noOfMatches);
        void RemoveItemFromSet(string setId, string item);
        bool RemoveItemFromSortedSet(string setId, string value);
        long RemoveRangeFromSortedSet(string setId, int minRank, int maxRank);


        //get 
        object Get(string key);
        IList<object> GetAll();
        IQueryable<T> GetAll<T>(string hash, string value, Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll<T>(string hash, string value);
        IDictionary<string, object> GetAll(IEnumerable<string> keys);
        Dictionary<string, string> GetAllEntriesFromHash(string hashId);
        List<string> GetAllItemsFromList(string listId);
        HashSet<string> GetAllItemsFromSet(string setId);
        List<string> GetAllItemsFromSortedSet(string setId);
        List<string> GetAllItemsFromSortedSetDesc(string setId);
        List<string> GetAllKeys();
        IDictionary<string, double> GetAllWithScoresFromSortedSet(string setId);
        string GetAndSetEntry(string key, string value);
        object GetById(object id);
        IList<object> GetByIds(ICollection ids);
        HashSet<string> GetDifferencesFromSet(string fromSetId, params string[] withSetIds);
        string GetEntry(string key);
        T GetFromHash<T>(object id);
        long GetHashCount(string hashId);
        List<string> GetHashKeys(string hashId);
        List<string> GetHashValues(string hashId);
        HashSet<string> GetIntersectFromSets(params string[] setIds);
        string GetItemFromList(string listId, int listIndex);
        long GetItemIndexInSortedSet(string setId, string value);
        long GetItemIndexInSortedSetDesc(string setId, string value);
        double GetItemScoreInSortedSet(string setId, string value);
        long GetListCount(string listId);
        List<string> GetRangeFromList(string listId, int startingFrom, int endingAt);
        List<string> GetRangeFromSortedList(string listId, int startingFrom, int endingAt);
        List<string> GetRangeFromSortedSet(string setId, int fromRank, int toRank);
        string GetValue(string key);
        string GetValueFromHash(string hashId, string key);
        List<string> GetValues(List<string> keys);
        List<T> GetValues<T>(List<string> keys);
        List<string> GetValuesFromHash(string hashId, params string[] keys);
        Dictionary<string, string> GetValuesMap(List<string> keys);
        Dictionary<string, T> GetValuesMap<T>(List<string> keys);

        //rename and replace
        void RenameKey(string fromName, string toName);
        void Rename(string oldKey, string newKey);
        bool Replace(string key, object value);
        bool Replace(string key, object value, DateTime expiresAt);
        bool Replace(string key, object value, TimeSpan expiresIn);
        //search
        List<string> SearchKeys(string pattern);
        //exec operations
        void Exec(Action<RedisClient> action);
        object Exec(Func<RedisClient, object> action);
        //unit of work
        void Save();
        void SaveAsync();
        void Shutdown();
        void SlaveOf(string hostname, int port);
        void SlaveOfNoOne();
        object[] Slowlog(int? top);
        void SlowlogReset();
        void SetClient(string name);
        void SetConfig(string configItem, string value);
        IDisposable AcquireLock(string key);
        IDisposable AcquireLock(string key, TimeSpan timeOut);

    }
}
