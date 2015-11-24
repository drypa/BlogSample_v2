using System;
using RabbitMQ.Client;

namespace Blog.BusinessLogic.RabbitMQ
{
    public class Producer<TMessage> : IDisposable
        where TMessage : new()
    {
        private readonly string exchangeName;
        private readonly string hostName;
        private readonly string queue;
        private readonly string routing;
        private IConnection connection;

        public Producer(string serverName, string queueName, string exchange, string routingKey)
        {
            hostName = serverName;
            queue = queueName;
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

        private void ConfigureChanel(IModel chanel)
        {
            chanel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: true, arguments: null);
        }

        private void Send(byte[] message)
        {
            using (IModel channel = Connection.CreateModel())
            {

                if (!string.IsNullOrEmpty(exchangeName))
                {
                    channel.QueueDeclare();
                    channel.ExchangeDeclare(exchangeName, global::RabbitMQ.Client.ExchangeType.Fanout);
                    channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, basicProperties: null, body: message);
                }
                else
                {
                    channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: true, arguments: null);
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = DeliveryMode.Persistent;
                    channel.BasicPublish(exchange: string.Empty, routingKey: queue, basicProperties: properties, body: message);
                }
            }
        }

        private static class DeliveryMode
        {
            public static byte NonPersistent
            {
                get { return 1; }
            }

            public static byte Persistent
            {
                get { return 2; }
            }
        }
    }
}
