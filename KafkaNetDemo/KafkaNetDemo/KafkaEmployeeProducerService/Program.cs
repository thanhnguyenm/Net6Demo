// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaEmployeeProducerService;
using System.Globalization;
using System.Net;
using TimeOff.Models.LeaveApplication;

var adminConfig = new AdminClientConfig { BootstrapServers = "127.0.0.1:9092" };
var schemaRegistryConfig = new SchemaRegistryConfig { Url = "http://127.0.0.1:8081" };
var produceConfig = new ProducerConfig
{
    BootstrapServers = "127.0.0.1:9092",
    EnableDeliveryReports = true,
    ClientId = Dns.GetHostName()
};

using var adminClient = new AdminClientBuilder(adminConfig).Build();
try
{
    await adminClient.CreateTopicsAsync(new[]
    {
        new TopicSpecification
        {
            Name = "leave-applications",
            ReplicationFactor = 1,
            NumPartitions = 3
        }
    });

}
catch (CreateTopicsException e) when (e.Results.Select(r => r.Error.Code).Any(el => el == ErrorCode.TopicAlreadyExists))
{
    Console.WriteLine($"Topic {e.Results[0].Topic} already exists");
}

using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
using var producer = new ProducerBuilder<string, LeaveApplicationReceived>(produceConfig)
       .SetKeySerializer(new AvroSerializer<string>(schemaRegistry))
       .SetValueSerializer(new AvroSerializer<LeaveApplicationReceived>(schemaRegistry))
       .Build();
while(true)
{
    var empEmail = ReadLine.Read("Enter your employee Email: ", "none@demo.com").ToLowerInvariant();
    var empDepartment = ReadLine.Read("Enter your department code (HR, IT, OPS): ").ToLowerInvariant();
    var leaveDurationInHours = int.Parse(ReadLine.Read("Enter number of hours of leave requested: "));
    var leaveStartDate = DateTime.ParseExact(ReadLine.Read("Enter vacation start date (dd-mm-yy): ", $"{DateTime.Today:dd-mm-yy}"), "dd-mm-yy", CultureInfo.InvariantCulture);

    var leaveApplication = new LeaveApplicationReceived
    {
        LeaveGuid = Guid.NewGuid().ToString(),
        EmpDepartment = empDepartment,
        EmpEmail = empEmail,
        LeaveDurationInHours = leaveDurationInHours,
        LeaveStartDateTicks = leaveStartDate.Ticks
    };

    var partition = new TopicPartition(
        "leave-applications",
        new Partition((int)Enum.Parse<Departments>(empDepartment?.ToUpper())));
    var result = await producer.ProduceAsync(partition,
        new Message<string, LeaveApplicationReceived>
        {
            Key = $"{empEmail}-{leaveApplication.LeaveGuid}",
            Value = leaveApplication
        });
    Console.WriteLine($"\nMsg: Your leave request is queued at offset {result.Offset.Value} in the Topic {result.Topic}:{result.Partition.Value}\n\n");
}