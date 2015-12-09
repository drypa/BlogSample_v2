using System;
using Blog.BusinessLogic.RabbitMQ;
using RabbitMQ.Client;
using ExchangeType = Blog.BusinessLogic.RabbitMQ.ExchangeType;

namespace Blog.BusinessLogic.Common.RabbitMQ
{
    public class Producer<TMessage> : IDisposable
        where TMessage : new()
    {
        private readonly string exchangeName;
        private readonly string hostName;
        private readonly string routing;
        private IConnection connection;

        public Producer(string serverName, string exchange, string routingKey)
        {
            hostName = serverName;
            exchangeName = exchange;
            routing = routingKey;
        }

        private IConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    var factory = new ConnectionFactory { HostName = hostName };
                    connection = factory.CreateConnection();
                }
                return connection;
            }
        }

        public void Send(TMessage obj)
        {
            byte[] message = new Serializer<TMessage>().Serialize(obj);
            Send(message);
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        private void Send(byte[] message)
        {
            using (IModel channel = Connection.CreateModel())
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.DeliveryMode = DeliveryMode.Persistent;
                


                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                channel.BasicPublish(exchange: exchangeName, routingKey: routing, basicProperties: properties, body: message);

            }
        }
    }
}
