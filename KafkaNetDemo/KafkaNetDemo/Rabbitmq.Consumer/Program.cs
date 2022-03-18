// See https://aka.ms/new-console-template for more information

using Rabbitmq.Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//QueueDeclare.Consume(channel);
//DirectExchange.Consume(channel);
TopicExchange.Consume(channel);
Console.ReadLine();