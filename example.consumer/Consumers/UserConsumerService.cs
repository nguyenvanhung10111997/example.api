using Autofac;
using example.domain.Entities;
using example.infrastructure.Configurations;
using example.service.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace example.consumer.Features
{
    public class UserConsumerService : BackgroundService, IHostedService
    {
        private readonly IUserService _userService;
        private readonly ILifetimeScope _lifetimeScope;
        public UserConsumerService(IUserService userService,
            ILifetimeScope lifetimeScope)
        {
            _userService = userService;
            _lifetimeScope = lifetimeScope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                ReceiveMessage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ReceiveMessage()
        {
            var factory = new ConnectionFactory
            {
                HostName = ApiConfig.Providers.RabbitMQ.Host,
                Port = 5672,
                UserName = ApiConfig.Providers.RabbitMQ.Username,
                Password = ApiConfig.Providers.RabbitMQ.Password
            };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.ExchangeDeclare("user_processing_exchange", type: ExchangeType.Fanout);
            channel.QueueDeclare(queue: "user_queue", exclusive: false);
            channel.QueueBind("user_queue", "user_processing_exchange", routingKey: "user");

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var user = JsonConvert.DeserializeObject<User>(message);
                var createUserResult = await _userService.CreateAsync(user);
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "user_queue", autoAck: false, consumer: consumer);
        }
    }
}
