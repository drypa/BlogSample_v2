using System;
using System.Configuration;
using System.Reflection;

namespace Blog.Client.PowerShellCmdlet
{
    internal class CmdletSettingsHelper
    {
        public static string SeviceUrl()
        {
            return GetSettingValue("SeviceUrl");
        }
        
        private static string GetSettingValue(string settingKey)
        {
            KeyValueConfigurationCollection settings =
                ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings;

            KeyValueConfigurationElement setting = settings[settingKey];
            return setting == null ? null : setting.Value;
        }
    }
}
