using INGA.Framework.Caching.Common;

namespace INGA.Framework.Caching.Memory
{
    public class MemcacheConnectionOptions : IConnectionOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
