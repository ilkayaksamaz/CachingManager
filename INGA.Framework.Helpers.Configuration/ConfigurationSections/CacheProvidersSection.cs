using System.Configuration;
using INGA.Framework.Helpers.Configuration.ConfigurationCollections;

namespace INGA.Framework.Helpers.Configuration.ConfigurationSections
{
    public class CacheProvidersSection : ConfigurationSection
    {
       
        [System.Configuration.ConfigurationProperty("CacheProviders")]
        [ConfigurationCollection(typeof(CacheProviderCollection))]
        public CacheProviderCollection CacheProviderCollection
        {
            get
            {
                object o = this["CacheProviders"];
                return o as CacheProviderCollection;
            }
        }
    }
}
