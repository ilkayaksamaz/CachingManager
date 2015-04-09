using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace INGA.Framework.Caching.Redis
{
    public class RedisProvider : IRedisProvider
    {
        readonly RedisClient _client;
        public RedisProvider(RedisClient client)
        {
            _client = client;
        }


        public object Store(object entity)
        {
            return _client.Store(entity);
        }

        public void StoreAll(IEnumerable<object> entities)
        {
            _client.StoreAll(entities);
        }

        public void StoreAsHash(object entity)
        {
            _client.StoreAsHash(entity);
        }

        public void StoreDifferencesFromSet(IRedisSet<object> intoSet, IRedisSet<object> fromSet, params IRedisSet<object>[] withSets)
        {
            List<string> ids = withSets.Select(set => set.Id).ToList();
            _client.StoreDifferencesFromSet(intoSet.Id, fromSet.Id, ids.ToArray());
        }

        public void StoreIntersectFromSets(IRedisSet<object> intoSet, params IRedisSet<object>[] sets)
        {
            List<string> ids = sets.Select(set => set.Id).ToList();
            _client.StoreIntersectFromSets(intoSet.Id, ids.ToArray());
        }

        public long StoreIntersectFromSortedSets(IRedisSortedSet<object> intoSetId, params IRedisSortedSet<object>[] setIds)
        {
            List<string> ids = setIds.Select(set => set.Id).ToList();
            return _client.StoreIntersectFromSortedSets(intoSetId.Id, ids.ToArray());
        }

        public object StoreObject(object obj)
        {
            return _client.StoreObject(obj);
        }

        public void StoreUnionFromSets(IRedisSet<object> intoSet, params IRedisSet<object>[] sets)
        {
            List<string> ids = sets.Select(set => set.Id).ToList();
            _client.StoreUnionFromSets(intoSet.Id, ids.ToArray());
        }

        public long StoreUnionFromSortedSets(IRedisSortedSet<object> intoSetId, params IRedisSortedSet<object>[] setIds)
        {
            List<string> ids = setIds.Select(set => set.Id).ToList();
            return _client.StoreUnionFromSortedSets(intoSetId.Id, ids.ToArray());
        }

        public bool Set(string key, object entity)
        {
            return _client.Set(key, entity);
        }

        public bool Set(string key, object entity, DateTime expireDate)
        {
            return _client.Set(key, entity, expireDate);
        }

        public bool Set(string key, object entity, TimeSpan expireTime)
        {
            return _client.Set(key, entity, expireTime);
        }

        public bool Set(string key, byte[] values)
        {
            return _client.Set(key, values);
        }

        public bool Set(string key, byte[] values, bool exists, int existingSecond, long existingMillisecond)
        {
            return _client.Set(key, values, exists, existingSecond, existingMillisecond);
        }

        public void Set(string key, byte[] values, int existingSecond, long existingMillisecond)
        {
            _client.Set(key, values, existingSecond, existingMillisecond);
        }

        public void SetAll(Dictionary<string, string> values)
        {
            _client.SetAll(values);
        }
        public void Set<T>(T item, string hash, string value, string keyName)
        {
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty(keyName);

            _client.SetEntryInHash(hash, prop.GetValue(item).ToString(), value.ToLower());

            _client.As<T>().Store(item);
        }
        public void Set<T>(T item, List<string> hash, List<string> value, string keyName)
        {
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty(keyName);

            for (int i = 0; i < hash.Count; i++)
            {
                _client.SetEntryInHash(hash[i], prop.GetValue(item).ToString(), value[i].ToLower());
            }

            _client.As<T>().Store(item);
        }


        public void SetAll<T>(List<T> list, string hash, string value, string keyName)
        {
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo prop = t.GetProperty(keyName);

                _client.SetEntryInHash(hash, prop.GetValue(item).ToString(), value.ToLower());

                _client.As<T>().StoreAll(list);
            }
        }
        public void SetAll<T>(List<T> list, List<string> hash, List<string> value, string keyName)
        {
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo prop = t.GetProperty(keyName);

                for (int i = 0; i < hash.Count; i++)
                {
                    _client.SetEntryInHash(hash[i], prop.GetValue(item).ToString(), value[i].ToLower());
                }

                _client.As<T>().StoreAll(list);
            }
        }
        public void SetAll(IEnumerable<string> firstValues, IEnumerable<string> secondValues)
        {
            _client.SetAll(firstValues, secondValues);
        }

        public long SetBit(string key, int offset, int value)
        {
            return _client.SetBit(key, offset, value);
        }

        public void SetEx(string key, int expireInSeconds, byte[] value)
        {
            _client.SetEx(key, expireInSeconds, value);
        }

        public long SetNx(string key, byte[] value)
        {
            return _client.SetNX(key, value);
        }

        public long SetRange(string key, int offset, byte[] value)
        {
            return _client.SetRange(key, offset, value);
        }

        public void SetEntry(string key, string value)
        {
            _client.SetEntry(key, value);
        }

        public void SetEntry(string key, string value, TimeSpan expireIn)
        {
            _client.SetEntry(key, value, expireIn);
        }

        public bool SetEntryIfExists(string key, string value)
        {
            return _client.SetEntryIfExists(key, value);
        }

        public bool SetEntryIfExists(string key, string value, TimeSpan expireIn)
        {
            return _client.SetEntryIfExists(key, value, expireIn);
        }

        public bool SetEntryIfNotExists(string key, string value)
        {
            return _client.SetEntryIfNotExists(key, value);
        }

        public bool SetEntryIfNotExists(string key, string value, TimeSpan expireIn)
        {
            return _client.SetEntryIfNotExists(key, value, expireIn);
        }

        public bool SetEntryInHash(string hashId, string key, string value)
        {
            return _client.SetEntryInHash(hashId, key, value);
        }

        public bool SetEntryInHashIfNotExists(string hashId, string key, string value)
        {
            return _client.SetEntryInHashIfNotExists(hashId, key, value);
        }

        public void SetItemInList(string listId, int listIndex, string value)
        {
            _client.SetItemInList(listId, listIndex, value);
        }

        public void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            _client.SetRangeInHash(hashId, keyValuePairs);
        }

        public bool SortedSetContainsItem(string setId, string value)
        {
            return _client.SortedSetContainsItem(setId, value);
        }

        public bool Add(string key, object value)
        {
            return _client.Add(key, value);
        }

        public bool Add(string key, object value, DateTime expiresAt)
        {
            return _client.Add(key, value, expiresAt);
        }

        public bool Add(string key, object value, TimeSpan expiresIn)
        {
            return _client.Add(key, value, expiresIn);
        }

        public void AddItemToList(string listId, string value)
        {
            _client.AddItemToList(listId, value);
        }

        public void AddItemToSet(string setId, string item)
        {
            _client.AddItemToSet(setId, item);
        }

        public bool AddItemToSortedSet(string setId, string value)
        {
            return _client.AddItemToSortedSet(setId, value);
        }

        public bool AddItemToSortedSet(string setId, string value, double score)
        {
            return _client.AddItemToSortedSet(setId, value, score);
        }

        public bool AddItemToSortedSet(string setId, string value, long score)
        {
            return _client.AddItemToSortedSet(setId, value, score);
        }

        public void AddRangeToList(string listId, List<string> values)
        {
            _client.AddRangeToList(listId, values);
        }

        public void AddRangeToSet(string setId, List<string> items)
        {
            _client.AddRangeToSet(setId, items);
        }

        public bool AddRangeToSortedSet(string setId, List<string> values, double score)
        {
            return _client.AddRangeToSortedSet(setId, values, score);
        }

        public bool AddRangeToSortedSet(string setId, List<string> values, long score)
        {
            return _client.AddRangeToSortedSet(setId, values, score);
        }

        public void Delete(object entity)
        {
            _client.Delete(entity);
        }

        public void DeleteAll()
        {
            _client.DeleteAll<object>();
        }

        public void DeleteById(object id)
        {
            _client.DeleteById<object>(id);
        }

        public void DeleteByIds(ICollection ids)
        {
            _client.DeleteByIds<object>(ids);
        }

        public bool Remove(string key)
        {
            return _client.Remove(key);
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            _client.RemoveAll(keys);
        }

        public void RemoveAllFromList(string listId)
        {
            _client.RemoveAllFromList(listId);
        }

        public string RemoveEndFromList(string listId)
        {
            return _client.RemoveEndFromList(listId);
        }

        public bool RemoveEntry(params string[] keys)
        {
            return _client.RemoveEntry(keys);
        }

        public bool RemoveEntryFromHash(string hashId, string key)
        {
            return _client.RemoveEntryFromHash(hashId, key);
        }

        public long RemoveItemFromList(string listId, string value)
        {
            return _client.RemoveItemFromList(listId, value);
        }

        public long RemoveItemFromList(string listId, string value, int noOfMatches)
        {
            return _client.RemoveItemFromList(listId, value, noOfMatches);
        }

        public void RemoveItemFromSet(string setId, string item)
        {
            _client.RemoveItemFromSet(setId, item);
        }

        public bool RemoveItemFromSortedSet(string setId, string value)
        {
            return _client.RemoveItemFromSortedSet(setId, value);
        }

        public long RemoveRangeFromSortedSet(string setId, int minRank, int maxRank)
        {
            return _client.RemoveRangeFromSortedSet(setId, minRank, maxRank);
        }



        public object Get(string key)
        {
            return _client.Get<object>(key);
        }

        public IList<object> GetAll()
        {
            return _client.GetAll<object>();
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            return _client.GetAll<object>(keys);
        }

        public Dictionary<string, string> GetAllEntriesFromHash(string hashId)
        {
            return _client.GetAllEntriesFromHash(hashId);
        }

        public List<string> GetAllItemsFromList(string listId)
        {
            return _client.GetAllItemsFromList(listId);
        }

        public HashSet<string> GetAllItemsFromSet(string setId)
        {
            return _client.GetAllItemsFromSet(setId);
        }

        public List<string> GetAllItemsFromSortedSet(string setId)
        {
            return _client.GetAllItemsFromSortedSet(setId);
        }

        public List<string> GetAllItemsFromSortedSetDesc(string setId)
        {
            return _client.GetAllItemsFromSortedSetDesc(setId);
        }

        public List<string> GetAllKeys()
        {
            return _client.GetAllKeys();
        }

        public IDictionary<string, double> GetAllWithScoresFromSortedSet(string setId)
        {
            return _client.GetAllWithScoresFromSortedSet(setId);
        }

        public string GetAndSetEntry(string key, string value)
        {
            return _client.GetAndSetEntry(key, value);
        }

        public object GetById(object id)
        {
            return _client.GetById<object>(id);
        }
        public IQueryable<T> GetAll<T>(string hash, string value, Expression<Func<T, bool>> filter)
        {
            var filtered = _client.GetAllEntriesFromHash(hash).Where(c => c.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            var ids = filtered.Select(c => c.Key);

            var ret = _client.As<T>().GetByIds(ids).AsQueryable()
                                .Where(filter);

            return ret;
        }
        public IQueryable<T> GetAll<T>(string hash, string value)
        {
            var filtered = _client.GetAllEntriesFromHash(hash).Where(c => c.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            var ids = filtered.Select(c => c.Key);

            var ret = _client.As<T>().GetByIds(ids).AsQueryable();
            return ret;
        }
        public IList<object> GetByIds(ICollection ids)
        {
            return _client.GetByIds<object>(ids);
        }

        public HashSet<string> GetDifferencesFromSet(string fromSetId, params string[] withSetIds)
        {
            return _client.GetDifferencesFromSet(fromSetId, withSetIds);
        }

        public string GetEntry(string key)
        {
            return _client.GetEntry(key);
        }

        public T GetFromHash<T>(object id)
        {
            return _client.GetFromHash<T>(id);
        }

        public long GetHashCount(string hashId)
        {
            return _client.GetHashCount(hashId);
        }

        public List<string> GetHashKeys(string hashId)
        {
            return _client.GetHashKeys(hashId);
        }

        public List<string> GetHashValues(string hashId)
        {
            return _client.GetHashValues(hashId);
        }

        public HashSet<string> GetIntersectFromSets(params string[] setIds)
        {
            return _client.GetIntersectFromSets(setIds);
        }

        public string GetItemFromList(string listId, int listIndex)
        {
            return _client.GetItemFromList(listId, listIndex);
        }

        public long GetItemIndexInSortedSet(string setId, string value)
        {
            return _client.GetItemIndexInSortedSet(setId, value);
        }

        public long GetItemIndexInSortedSetDesc(string setId, string value)
        {
            return _client.GetItemIndexInSortedSetDesc(setId, value);
        }

        public double GetItemScoreInSortedSet(string setId, string value)
        {
            return _client.GetItemScoreInSortedSet(setId, value);
        }

        public long GetListCount(string listId)
        {
            return _client.GetListCount(listId);
        }

        public List<string> GetRangeFromList(string listId, int startingFrom, int endingAt)
        {
            return _client.GetRangeFromList(listId, startingFrom, endingAt);
        }

        public List<string> GetRangeFromSortedList(string listId, int startingFrom, int endingAt)
        {
            return _client.GetRangeFromSortedList(listId, startingFrom, endingAt);
        }

        public List<string> GetRangeFromSortedSet(string setId, int fromRank, int toRank)
        {
            return _client.GetRangeFromSortedSet(setId, fromRank, toRank);
        }

        public string GetValue(string key)
        {
            return _client.GetValue(key);
        }

        public string GetValueFromHash(string hashId, string key)
        {
            return _client.GetValueFromHash(hashId, key);
        }

        public List<string> GetValues(List<string> keys)
        {
            return _client.GetValues(keys);
        }

        public List<T1> GetValues<T1>(List<string> keys)
        {
            return _client.GetValues<T1>(keys);
        }

        public List<string> GetValuesFromHash(string hashId, params string[] keys)
        {
            return _client.GetValuesFromHash(hashId, keys);
        }

        public Dictionary<string, string> GetValuesMap(List<string> keys)
        {
            return _client.GetValuesMap(keys);
        }

        public Dictionary<string, T1> GetValuesMap<T1>(List<string> keys)
        {
            return _client.GetValuesMap<T1>(keys);
        }

        public void RenameKey(string fromName, string toName)
        {
            _client.RenameKey(fromName, toName);
        }
        public void Rename(string oldName, string newName)
        {
            _client.Rename(oldName, newName);
        }
        public bool Replace(string key, object value)
        {
            return _client.Replace(key, value);
        }

        public bool Replace(string key, object value, DateTime expiresAt)
        {
            return _client.Replace(key, value, expiresAt);
        }

        public bool Replace(string key, object value, TimeSpan expiresIn)
        {
            return _client.Replace(key, value, expiresIn);
        }


        public List<string> SearchKeys(string pattern)
        {
            return _client.SearchKeys(pattern);
        }


        public void Exec(Action<RedisClient> action)
        {
            _client.Exec(action);
        }

        public object Exec(Func<RedisClient, object> action)
        {
            return _client.Exec(action);
        }

        public void Save()
        {
            _client.Save();
        }

        public void SaveAsync()
        {
            _client.SaveAsync();
        }

        public void Shutdown()
        {
            _client.Shutdown();
        }

        public void SlaveOf(string hostname, int port)
        {
            _client.SlaveOf(hostname, port);
        }

        public void SlaveOfNoOne()
        {
            _client.SlaveOfNoOne();
        }

        public object[] Slowlog(int? top)
        {
            return _client.Slowlog(top);
        }

        public void SlowlogReset()
        {
            _client.SlowlogReset();
        }

        public void SetClient(string name)
        {
            _client.SetClient(name);
        }

        public void SetConfig(string configItem, string value)
        {
            _client.SetConfig(configItem, value);
        }

        public IDisposable AcquireLock(string key)
        {
            return _client.AcquireLock(key);
        }

        public IDisposable AcquireLock(string key, TimeSpan timeOut)
        {
            return _client.AcquireLock(key, timeOut);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        
    }
}
