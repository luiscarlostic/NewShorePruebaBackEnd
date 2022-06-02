using System.Configuration;

namespace NewshoreAir.Domain.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetKey(string key)
        {
            string item = ConfigurationManager.AppSettings[key];
            if (item == null)
            {
                throw new ConfigurationErrorsException($"Configuration key {key} is not available");
            }
            return item;
        }
    }
}
