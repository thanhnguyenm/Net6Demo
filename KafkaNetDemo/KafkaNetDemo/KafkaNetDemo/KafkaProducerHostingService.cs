// See https://aka.ms/new-console-template for more information



using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class KafkaProducerHostingService : IHostedService
{
    private readonly ILogger<KafkaProducerHostingService> _logger;
    private IProducer<Null, string> _producer;
    public KafkaProducerHostingService(ILogger<KafkaProducerHostingService> logger)
    {
        _logger = logger;
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    async Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 100; i++)
        {
            var value = $"Message {i}";
            _logger.LogInformation(value);

            await _producer.ProduceAsync(topic: "demo", new Message<Null, string>
            {
                Value = value,
            }, cancellationToken);

            _producer.Flush(TimeSpan.FromSeconds(10));
        }
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        _producer?.Dispose();
        return Task.CompletedTask;
    }
}
