using System;
using INGA.Framework.Caching.Common;
using ServiceStack.Redis;

namespace INGA.Framework.Caching.Redis
{
   public class RedisConnectionOptions : IConnectionOptions
    {
        public RedisEndpoint EndPoint { get; set; }
        public Uri Uri { get; set; }
        public int DbLocation { get; set; }

       #region Implementation of IConnectionOptions

       public string Host { get; set; }

       public int Port { get; set; }

       public string Username { get; set; }

       public string Password { get; set; }

       #endregion
    }
}
