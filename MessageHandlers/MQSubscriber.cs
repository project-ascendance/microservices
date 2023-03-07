using MessageHandlers.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers
{
    public class MQSubscriber : IMQSubscriber
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        public MQSubscriber(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _consumer = new EventingBasicConsumer(_channel);
        }

        public IMQSubscriber AddExchange(string exchange)
        {
            _channel.ExchangeDeclare(exchange, type: ExchangeType.Fanout);
            return this;
        }
        
        public IMQSubscriber AddQueue(string queue)
        {
            _channel.QueueDeclare(queue, false, false, false);
            return this;
        }

        public IMQSubscriber QueueSubscribe(string queue, string exchange)
        {
            _channel.QueueBind(queue, exchange, "");
            return this;
        }

        public Task ConsumeAsync(string queue)
        {
            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            _channel.BasicConsume(queue, true, _consumer);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
