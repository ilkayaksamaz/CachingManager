using System.Configuration;

namespace INGA.Framework.Helpers.Configuration.ConfigurationElements
{
    public class CacheProviders: ConfigurationElement
    {
        public CacheProviders() { }

        public CacheProviders(string name, string host, int port, string username, string password, string isActive,string cacheName)
        {
            Name = name;
            Host = host;
            Port = port;
            Username = username;
            Password = password;
            IsActive = isActive;
            CacheName = cacheName;
        }

      

        [ConfigurationProperty("Name", DefaultValue = "*", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("Host", DefaultValue = "*", IsRequired = true, IsKey = false)]
        public string Host
        {
            get { return (string)this["Host"]; }
            set { this["Host"] = value; }
        }

        [ConfigurationProperty("Port", DefaultValue = 0, IsRequired = true, IsKey = false)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }


        [ConfigurationProperty("Username", DefaultValue = "*", IsRequired = true, IsKey = false)]
        public string Username
        {
            get { return (string)this["Username"]; }
            set { this["Username"] = value; }
        }


        [ConfigurationProperty("Password", DefaultValue = "*", IsRequired = true, IsKey = false)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }

        [ConfigurationProperty("IsActive", DefaultValue = "*", IsRequired = true, IsKey = false)]
        public string IsActive
        {
            get { return (string)this["IsActive"]; }
            set { this["IsActive"] = value; }
        }

        [ConfigurationProperty("CacheName", DefaultValue = "*", IsRequired = true, IsKey = false)]
        public string CacheName
        {
            get { return (string)this["CacheName"]; }
            set { this["CacheName"] = value; }
        }
    }
}
