using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Drawing;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        ReceiveMessage();
    }

    private static void ReceiveMessage()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        var connection = factory.CreateConnection();

        using (var channel = connection.CreateModel())
        {
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("product", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Product message received: {message}");
            };

            channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
        }
    }
}
