
using System.Configuration;
using System.Web.Configuration;

namespace INGA.Framework.Helpers.Configuration
{
    public static class ConfigurationHelper
    {
        private static System.Configuration.Configuration _config;
        public static System.Configuration.Configuration Config
        {
            get
            {
                return _config ?? (_config = WebConfigurationManager.OpenWebConfiguration(@"~"));
            }
        }

        public static T GetSection<T>(string sectionName) where T :
                                                      ConfigurationSection
        {
            T options = null;
            try
            {
                options = Config.GetSection(sectionName) as T;
            }
            catch
            {
            }

            return options;
        }

        public static void AddSection(string name, ConfigurationSection section)
        {
            _config.Sections.Add(name, section);
            Config.Save(ConfigurationSaveMode.Full);
        }

        public static void Save()
        {
            Config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
