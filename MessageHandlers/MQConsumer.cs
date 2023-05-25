using MessageHandlerAPIs.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlerAPIs
{
    public class MQConsumer : IMQConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public EventingBasicConsumer Consumer;
        private string _message = string.Empty;

        public IConnectionFactory ConnectionFactory { get; }

        public event Action<string> MessageReceived;

        public MQConsumer(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            _connection = ConnectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            Consumer = new EventingBasicConsumer(_channel);

            Consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                _message = Encoding.UTF8.GetString(body);

                MessageReceived?.Invoke(_message);
            };
        }

        public IMQConsumer AddExchange(string exchange)
        {
            _channel.ExchangeDeclare(exchange, type: ExchangeType.Fanout);
            return this;
        }

        public IMQConsumer AddQueue(string queue)
        {
            _channel.QueueDeclare(queue, false, false, false);
            return this;
        }

        public IMQConsumer QueueBind(string queue, string exchange)
        {
            _channel.QueueBind(queue, exchange, "");
            return this;
        }

        public Task Consume(string queue)
        {
            _channel.BasicConsume(queue, true, Consumer);
            return Task.CompletedTask;

        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
