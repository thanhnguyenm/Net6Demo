using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rabbitmq.Producer
{
    internal static class QueueDeclare
    {
        public static void Produce(IModel channel)
        {
            channel.QueueDeclare("demo1",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            while (true)
            {
                var msg = ReadLine.Read("Enter message: ");
                var message = new { Id = Guid.NewGuid().ToString(), Name = "Producer", Message = msg };
                var messageBinary = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("", "demo1", null, messageBinary);
            }

        }
    }
}
