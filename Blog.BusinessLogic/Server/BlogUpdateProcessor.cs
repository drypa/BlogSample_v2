using System;
using Blog.BusinessLogic.RabbitMQ;

namespace Blog.BusinessLogic.Server
{
    public abstract class BlogUpdateProcessor
    {
        private readonly ExchangeConfiguration exchangeConfiguration;

        public BlogUpdateProcessor(ExchangeConfiguration configuration)
        {
            exchangeConfiguration = configuration;
        }

        protected Producer<T> GetProducer<T>(T request) where T : new()
        {
            return new Producer<T>(exchangeConfiguration.ServerName, exchangeConfiguration.ExchangeType, exchangeConfiguration.GetRouting(request.GetType()));
        }
    }
}
