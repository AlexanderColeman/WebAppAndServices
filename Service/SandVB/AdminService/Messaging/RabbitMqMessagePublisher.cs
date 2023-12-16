using AdminService.Messaging.Interface;
using Microsoft.Extensions.Options;
using ModelSharingService.IntegrationEvents;
using ModelSharingService.Message;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace AdminService.Messaging
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly string _userEventExchange = Exchanges.UserEvent;
        private readonly object _usersLock = new object();
        private IConnection _connection;
        private IModel _userEventChannel;

        private ConnectionFactory _factory;

        public RabbitMqMessagePublisher(IOptionsMonitor<RabbitMqOptions> optionsAccessor)
        {
            _rabbitMqOptions = optionsAccessor.CurrentValue;
            Initialize();
        }

        private void Initialize()
        {
            _factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.HostName,
                Port = _rabbitMqOptions.Port,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password,
                // rabbitmq ssl configuration settings
                Ssl = new SslOption()
                {
                    Enabled = _rabbitMqOptions.SslEnabled,
                    ServerName = _rabbitMqOptions.SslServerName
                }
            };
            _connection = _factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _userEventChannel = _connection.CreateModel();
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _userEventChannel.Close();
            _connection.Close();
        }

        public void PublishUserEvent(UserEvent userEvent)
        {
            // concurrency lock
            lock (_usersLock)
            {
                var json = JsonSerializer.Serialize<UserEvent>(userEvent);
                Publish(json, "", _userEventExchange, true);
            }
        }


        private void Publish(string json, string routingKey, string exchange, bool isExchange)
        {
            var body = Encoding.UTF8.GetBytes(json);

            if (isExchange)
            {
                _userEventChannel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
            }
            else
            {
                _userEventChannel.QueueDeclare(queue: routingKey, durable: false, exclusive: false, autoDelete: false, arguments: null);
                _userEventChannel.BasicPublish(exchange: "", routingKey: routingKey, basicProperties: null, body: body);
            }
        }
    }
}
