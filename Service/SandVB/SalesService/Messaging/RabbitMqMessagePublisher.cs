using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using SalesService.Messaging.Interface;
using ModelSharingService.Message;
using ModelSharingService.IntegrationEvents;

namespace SalesService.Messaging
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly string _aircraftEventExchange = Exchanges.UserEvent;
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
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        { }

        public void PublishAircraftEvent(UserEvent userEvent)
        {
            var json = JsonSerializer.Serialize<UserEvent>(userEvent);
            Publish(json, "", _aircraftEventExchange, true);
        }

        private void Publish(string json, string routingKey, string exchange, bool isExchange)
        {
            var body = Encoding.UTF8.GetBytes(json);

            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            if (isExchange)
            {
                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
            }
            else
            {
                channel.QueueDeclare(queue: routingKey, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicPublish(exchange: "", routingKey: routingKey, basicProperties: null, body: body);
            }
        }
    }
}
