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
        private readonly string queue;
        private readonly string routing;
        private readonly string exchange;
        private IModel model;
        private IConnection connection;
        private EventingBasicConsumer consumer;

        public Consumer(string serverName, string queueName, string exchangeName, string routingKey, Action<TMessage> onMessageReceived)
        {
            HostName = serverName;
            queue = queueName;
            exchange = exchangeName;
            messageReceivedAction = onMessageReceived;
            routing = routingKey;
        }

        public void Open()
        {
            var factory = new ConnectionFactory { HostName = HostName };
            connection = factory.CreateConnection();
            model = connection.CreateModel();

            if (string.IsNullOrEmpty(exchange))
            {
                model.QueueDeclare(queue, true, false, false, null);
            }
            else
            {
                model.ExchangeDeclare(exchange, global::RabbitMQ.Client.ExchangeType.Fanout);
                var queueName = model.QueueDeclare().QueueName;
                model.QueueBind(queueName, exchange, routing);
            }

            consumer = new EventingBasicConsumer(model);

            consumer.Received += (sender, args) => ItemProcessing(args);
            model.BasicQos(0, 2, true);
            model.BasicConsume(queue, false, consumer);

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
