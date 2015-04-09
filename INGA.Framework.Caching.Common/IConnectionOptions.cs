namespace INGA.Framework.Caching.Common
{
    public interface IConnectionOptions
    {
         string Host { get; set; }
         int Port { get; set; }
         string Username { get; set; }
         string Password { get; set; }
    }
}
