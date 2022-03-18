// See https://aka.ms/new-console-template for more information

using Rabbitmq.Producer;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//QueueDeclare.Produce(channel);
//DirectExchange.Produce(channel);
TopicExchange.Produce(channel);