using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using OrderDbContext;

namespace Billing
{    
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing";
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseNServiceBus(context =>
                       {
                           //For Learning
                           //var endpointConfiguration = new EndpointConfiguration("Billing");

                           //endpointConfiguration.UseTransport<LearningTransport>();

                           //endpointConfiguration.SendFailedMessagesTo("error");
                           //endpointConfiguration.AuditProcessedMessagesTo("audit");
                           //endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

                           //var metrics = endpointConfiguration.EnableMetrics();
                           //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

                           //Azure Queue and Azure Storage
                           var azStorage = context.Configuration.GetConnectionString("AzureStorage");
                           var endpointName = "thanhnm-74DE141E26BC4449BC9A47CC8CFFB416-Billing";
                           var endpointConfiguration = new EndpointConfiguration(endpointName);
                           Console.WriteLine($"Host is connected to queue name : {endpointName}");

                           var transport = new AzureStorageQueueTransport(azStorage, useNativeDelayedDeliveries: false)
                           {
                               MessageWrapperSerializationDefinition = new NewtonsoftJsonSerializer()
                           };
                           var routingSettings = endpointConfiguration.UseTransport(transport);
                           routingSettings.DisablePublishing();
                           var persistence = endpointConfiguration.UsePersistence<AzureTablePersistence>();
                           persistence.ConnectionString(azStorage);
                           endpointConfiguration.Recoverability().Delayed(settings => settings.NumberOfRetries(0));
                           endpointConfiguration.EnableInstallers();


                           return endpointConfiguration;
                       })
                       .ConfigureServices((context, services) =>
                       {
                           var configuration = (IConfiguration)context.Configuration;
                           services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderDatabase")));
                       });
        }
    }
}
