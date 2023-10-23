using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example.infrastructure.RabbitMQ
{
    public interface IRabbitMQProducer : IDisposable
    {
        public void SendMessage<T>(T message);
    }
}
