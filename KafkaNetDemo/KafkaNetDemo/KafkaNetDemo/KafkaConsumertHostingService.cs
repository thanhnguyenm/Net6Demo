// See https://aka.ms/new-console-template for more information



using Confluent.Kafka;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

public class KafkaConsumertHostingService : IHostedService
{
    private readonly ILogger<KafkaConsumertHostingService> _logger;
    //private ClusterClient _clusterClient;
    private IConsumer<Null, string> _consumer;
    public KafkaConsumertHostingService(ILogger<KafkaConsumertHostingService> logger)
    {
        _logger = logger;
        //_clusterClient = new ClusterClient(new Configuration
        //{
        //    Seeds = "localhost:9092"
        //}, new ConsoleLogger());
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "test"
        };
        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    }

    async Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        //_clusterClient.ConsumeFromLatest("demo");
        //_clusterClient.MessageReceived += record =>
        //{
        //    _logger.LogInformation($"Received: {Encoding.UTF8.GetString(record.Value as byte[])}");
        //};
        int totalWaitTimeInMiliseconds = 0;
        int MaxTestWaitTimeInMiliseconds = 1000 * 20;
        int waitSingle = 200;
        _consumer.Subscribe("demo");
        while (true)
        {
            Thread.Sleep(waitSingle);
            var response = _consumer.Consume(TimeSpan.FromMilliseconds(100));
            if (response != null)
            {
                _logger.LogInformation($"Received: {response.Message?.Value}");
            }

            totalWaitTimeInMiliseconds += waitSingle;
            if (totalWaitTimeInMiliseconds >= MaxTestWaitTimeInMiliseconds)
            {
                break;
            }
        }
        await Task.CompletedTask;
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        //_clusterClient?.Dispose();
        _consumer?.Dispose();
        return Task.CompletedTask;
    }
}