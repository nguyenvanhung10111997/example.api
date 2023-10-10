using example.infrastructure.Configurations;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using System.Text;

namespace example.infrastructure.RabbitMQ
{
    internal class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            var retry = Policy
           .Handle<Exception>()
           .WaitAndRetry(2, retryAttempt => TimeSpan.FromMinutes(Math.Pow(2, retryAttempt)));

            retry.Execute(() =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = ApiConfig.Providers.RabbitMQ.Host,
                    UserName = ApiConfig.Providers.RabbitMQ.Username,
                    Password = ApiConfig.Providers.RabbitMQ.Password
                };

                var connection = factory.CreateConnection();

                using (var channel = connection.CreateModel())
                {
                    //declare the queue after mentioning name and a few property related to that
                    channel.QueueDeclare("product", exclusive: false);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    channel.BasicPublish(exchange: "", routingKey: "product", body: body);
                }
            });
        }

        #region Destructor
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RabbitMQProducer() { Dispose(false); }
        #endregion
    }
}
