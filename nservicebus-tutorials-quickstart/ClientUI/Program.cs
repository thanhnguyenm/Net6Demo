using System;
using Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace ClientUI
{   
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "ClientUI";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                        .UseNServiceBus(context =>
                        {
                            //For Learning
                            //var endpointConfiguration = new EndpointConfiguration("ClientUI");
                            //var transport = endpointConfiguration.UseTransport<LearningTransport>();

                            //var routing = transport.Routing();
                            //routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

                            //endpointConfiguration.SendFailedMessagesTo("error");
                            //endpointConfiguration.AuditProcessedMessagesTo("audit");
                            //endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

                            //var metrics = endpointConfiguration.EnableMetrics();
                            //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

                            //Azure Queue and Azure Storage
                            var azStorage = context.Configuration.GetConnectionString("AzureStorage");
                            var endpointName = "thanhnm-74DE141E26BC4449BC9A47CC8CFFB416-clientui";
                            var endpointConfiguration = new EndpointConfiguration(endpointName);
                            Console.WriteLine($"Host is connected to queue name : {endpointName}");

                            var transport = new AzureStorageQueueTransport(azStorage)
                            {
                                MessageWrapperSerializationDefinition = new NewtonsoftJsonSerializer()
                            };
                            var routingSettings = endpointConfiguration.UseTransport(transport);
                            routingSettings.DisablePublishing();
                            var persistence = endpointConfiguration.UsePersistence<AzureTablePersistence>();
                            persistence.ConnectionString(azStorage);
                            //endpointConfiguration.Recoverability().Delayed(settings => settings.NumberOfRetries(0));
                            endpointConfiguration.EnableInstallers();

                            return endpointConfiguration;

                        })
                       .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        });
        }
    }
}
