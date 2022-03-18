using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq.Consumer
{
    internal static class QueueDeclare
    {
        public static void Consume(IModel channel)
        {
            channel.QueueDeclare("demo1",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (send, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo1", true, consumer);
        }
    }
}
