using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Blog.BusinessLogic.RabbitMQ
{
    public class Consumer<TMessage> : IDisposable
        where TMessage : new()
    {
        private readonly string HostName;
        private readonly Action<TMessage> messageReceivedAction;
        private readonly string routing;
        private readonly string exchange;
        private IModel model;
        private IConnection connection;
        private EventingBasicConsumer consumer;

        public Consumer(string serverName, string exchangeName, string routingKey, Action<TMessage> onMessageReceived)
        {
            HostName = serverName;
            exchange = exchangeName;
            messageReceivedAction = onMessageReceived;
            routing = routingKey;
        }

        public void Open()
        {
            var factory = new ConnectionFactory { HostName = HostName };
            connection = factory.CreateConnection();
            model = connection.CreateModel();
            IBasicProperties properties = model.CreateBasicProperties();
            properties.DeliveryMode = DeliveryMode.Persistent;

            model.ExchangeDeclare(exchange, ExchangeType.Direct);
            var queueName = model.QueueDeclare().QueueName;
            model.BasicQos(0, 1, true);
            model.QueueBind(queue: queueName,
                exchange: exchange,
                routingKey: routing);

            consumer = new EventingBasicConsumer(model);

            consumer.Received += (sender, args) => ItemProcessing(args);
            
            model.BasicConsume(queueName, false, consumer);

        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
            }
            if (model != null)
            {
                model.Close();
            }
        }


        private void ItemProcessing(BasicDeliverEventArgs e)
        {
            TMessage message = new Serializer<TMessage>().Desearalize(e.Body);
            messageReceivedAction(message);
            if (model != null)
            {
                model.BasicAck(e.DeliveryTag, false);
            }
        }
    }
}
