using SalesService.Messaging.Interface;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using ModelSharingService.IntegrationEvents;
using SalesService.Manager.Interface;

namespace SalesService.Messaging
{
    public static class Exchnages { public static string UserEvent = "UserEventExchange"; }

    public class RabbitMqMessageSubscriber : BackgroundService, IMessageSubscriber
    {
        private readonly RabbitMqOptions _rabbitMqOptions;

        private IServiceProvider _serviceProvider;  // use this to get service with lifetimes independent of those created in startup
        private readonly string _userEventExchange = Exchnages.UserEvent;
        private readonly string _userEventQueue = "UserEventQueue_Sales";

        private IConnection _connection;
        private IModel _userEventChannel;

        private readonly object _userLock = new object();

        public RabbitMqMessageSubscriber(IOptionsMonitor<RabbitMqOptions> optionsAccessor, IServiceProvider serviceProvider)
        {
            _rabbitMqOptions = optionsAccessor.CurrentValue;
            _serviceProvider = serviceProvider;
            InitializeRabbitMqSubscriber();
        }

        private void InitializeRabbitMqSubscriber()
        {
            try
            {
                var factory = new ConnectionFactory
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

                _connection = factory.CreateConnection();
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                _userEventChannel = _connection.CreateModel();

                // declare the user event exchange, then the queue, then bind the exchange to the queue
                _userEventChannel.ExchangeDeclare(_userEventExchange, ExchangeType.Fanout);
                _userEventChannel.QueueDeclare(queue: _userEventQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                _userEventChannel.QueueBind(queue: _userEventQueue, exchange: _userEventExchange, routingKey: "");
            }

            catch (Exception ex)
            {
                throw new Exception("Error initializing RabbitMQ subscriber: " + ex.Message);
            }
        }

        public void ProcessUserEvent(IServiceProvider serviceProvider, UserEvent userEvent)
        {
            // this allows creation of scope and service lifetimes that are independent of those created in startup; there are confilcts if you try to inject
            // scoped services into this background service running as a singleton, so you need to create them here and then they are disposed immediately
            using (var scope = serviceProvider.CreateScope())
            {
                var adminManager = scope.ServiceProvider.GetRequiredService<IAdminManager>();
                // delegate processing to manager layer
                adminManager.ProcessUserEventAsync(userEvent).GetAwaiter().GetResult();
            }
        }


        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        { }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var userEventMessageConsumer = new EventingBasicConsumer(_userEventChannel);
            userEventMessageConsumer.Received += (ch, ea) => UserEventHandler(ea);
            _userEventChannel.BasicConsume(_userEventQueue, false, userEventMessageConsumer);

            return Task.CompletedTask;
        }

        private void UserEventHandler(BasicDeliverEventArgs ea)
        {
            lock (_userLock)
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var userEvent = JsonSerializer.Deserialize<UserEvent>(content);
                ProcessUserEvent(_serviceProvider, userEvent);
                _userEventChannel.BasicAck(ea.DeliveryTag, false);
            }
        }

        public override void Dispose()
        {
            _userEventChannel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
