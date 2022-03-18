using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rabbitmq.Producer
{
    internal class DirectExchange
    {
        public static void Produce(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);
            
            while (true)
            {
                var msg = ReadLine.Read("Enter message: ");
                var message = new { Id = Guid.NewGuid().ToString(), Name = "Producer", Message = msg };
                var messageBinary = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("demo-direct-exchange", "exchange.routing", null, messageBinary);
            }

        }
    }
}
