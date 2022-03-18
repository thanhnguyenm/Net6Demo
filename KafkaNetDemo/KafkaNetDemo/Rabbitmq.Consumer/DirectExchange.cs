﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq.Consumer
{
    internal class DirectExchange
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare("demo-direct-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            channel.QueueBind("demo-direct-queue", "demo-direct-exchange", "exchange.routing");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (send, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-direct-queue", true, consumer);
        }
    }
}
