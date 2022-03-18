// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using TimeOff.Models.LeaveApplication;

var schemaRegistryConfig = new SchemaRegistryConfig { Url = "http://127.0.0.1:8081" };
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "127.0.0.1:9092",
    GroupId = "manager",
    EnableAutoCommit = false,
    EnableAutoOffsetStore = false,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    MaxPollIntervalMs = 1000 * 60,
    SessionTimeoutMs = 1000 * 45
};

var leaveApplicationReceivedMessages = new Queue<KafkaMessage>();

using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
using var consumer = new ConsumerBuilder<string, LeaveApplicationReceived>(consumerConfig)
    .SetKeyDeserializer(new AvroDeserializer<string>(schemaRegistry).AsSyncOverAsync())
    .SetValueDeserializer(new AvroDeserializer<LeaveApplicationReceived>(schemaRegistry).AsSyncOverAsync())
    .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
    .Build();
{
    try
    {
        consumer.Subscribe("leave-applications");
        Console.WriteLine("Consumer loop started...");
        while (true)
        {
            try
            {
                var result = consumer.Consume(TimeSpan.FromMilliseconds((double)(1000 * 10)));
                var leaveRequest = result?.Message?.Value;
                if(leaveRequest == null)
                {
                    continue;
                }

                var mesage = new KafkaMessage(result.Message.Key, result.Partition.Value, result.Message.Value);
                leaveApplicationReceivedMessages.Enqueue(mesage);
                Console.WriteLine($"Received: {mesage}");

                consumer.Commit(result);
                consumer.StoreOffset(result);
            }
            catch (ConsumeException e)  when (!e.Error.IsFatal)
            {
                Console.WriteLine($"Non faltal error: {e}");
            }
        }
    }
    finally
    {
        consumer.Close();
    }
}

record KafkaMessage(string Key, int Partition, LeaveApplicationReceived Message);