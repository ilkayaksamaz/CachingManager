using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using INGA.Framework.Helpers.Configuration.ConfigurationElements;

namespace INGA.Framework.Helpers.Configuration.ConfigurationCollections
{
    public class CacheProviderCollection : ConfigurationElementCollection, IEnumerable<CacheProviders>
    {
        public CacheProviderCollection()
        {
            Console.WriteLine("CacheProviderCollection Constructor");
        }

        public CacheProviders this[int index]
        {
            get { return (CacheProviders)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(CacheProviders cacheProviderCollection)
        {
            BaseAdd(cacheProviderCollection);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheProviders();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheProviders)element).Name;
        }

        public void Remove(CacheProviders cacheProviderCollection)
        {
            BaseRemove(cacheProviderCollection.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        #region Implementation of IEnumerable<out CacheProviders>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<CacheProviders> GetEnumerator()
        {
            return this.BaseGetAllKeys().Select(key => (CacheProviders)BaseGet(key)).GetEnumerator();
        }

        #endregion
    }
}
