using System;
using System.Configuration;
using Blog.BusinessLogic;

namespace Blog.Service
{
    internal class AppSettingsHelper : IAppSettingsHelper
    {
        private readonly string ConnectionstringName = "BlogDb";

        public string GetConnectionString()
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            ConnectionStringSettings setting = settings[ConnectionstringName];
            if (setting == null)
            {
                throw new ConfigurationErrorsException(string.Format("Не найдена строка подключения '{0}' к базе данных", ConnectionstringName));
            }
            return setting.ConnectionString;
        }
    }
}
