using System.Collections.Specialized;

namespace INGA.Framework.Caching.InMemory
{
    public class InMemoryConnectionOptions
    {
        public string Name { get; set; }
        public NameValueCollection Config { get; set; }
    }
}
