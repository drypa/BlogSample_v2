using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.BusinessLogic
{
    public class ExchangeConfiguration
    {
        private readonly string exchangeType;
        private readonly Dictionary<Type, string> routingMap = new Dictionary<Type, string>();
        private readonly string serverName;

        public ExchangeConfiguration(string serverName, string exchangeType)
        {
            this.serverName = serverName;
            this.exchangeType = exchangeType;
        }

        public string ExchangeType
        {
            get { return exchangeType; }
        }

        public string ServerName
        {
            get { return serverName; }
        }

        public ExchangeConfiguration AddRouting(Type type,string routing)
        {
            routingMap.Add(type,routing);
            return this;
        }

        public string GetRouting(Type type)
        {
            return routingMap[type];
        }
        public Type GetMessageTypeForRoute(string route)
        {
            return routingMap.First(x => x.Value == route).Key;
        }
    }
}
