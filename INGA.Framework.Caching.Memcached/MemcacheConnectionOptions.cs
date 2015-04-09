﻿using INGA.Framework.Caching.Common;

namespace INGA.Framework.Caching.Memcached
{
    public class MemcachedConnectionOptions : IConnectionOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
