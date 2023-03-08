using MessageHandlerAPIs.Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlerAPIs
{
    public class MQPublisher : IMQPublisher
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MQPublisher(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public IMQPublisher AddExchange(string exchange)
        {
            _channel.ExchangeDeclare(exchange, type: ExchangeType.Fanout);
            return this;
        }

        public Task PublishAsync(string exchange, string JsonBody)
        {
            var body = Encoding.UTF8.GetBytes(JsonBody);

            _channel.BasicPublish(exchange, "", null, body);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
