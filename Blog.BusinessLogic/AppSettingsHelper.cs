using System;
using System.Configuration;

namespace Blog.BusinessLogic
{
    public class AppSettingsHelper
    {
        public string GetConnectionString(string name)
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            ConnectionStringSettings setting = settings[name];
            if (setting == null)
            {
                throw new ArgumentOutOfRangeException(name);
            }
            return setting.ConnectionString;
        }
    }
}
