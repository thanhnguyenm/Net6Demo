using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using OrderDbContext;

namespace Sales
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Sales";
            //await CreateHostBuilder(args).RunConsoleAsync();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseNServiceBus(context =>
                       {
                           //var endpointConfiguration = new EndpointConfiguration("Sales");

                           //endpointConfiguration.UseTransport<LearningTransport>();

                           //endpointConfiguration.SendFailedMessagesTo("error");
                           //endpointConfiguration.AuditProcessedMessagesTo("audit");
                           //endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

                           //// So that when we test recoverability, we don't have to wait so long
                           //// for the failed message to be sent to the error queue
                           //var recoverablility = endpointConfiguration.Recoverability();
                           //recoverablility.Delayed(
                           //    delayed =>
                           //    {
                           //        delayed.TimeIncrease(TimeSpan.FromSeconds(2));
                           //    }
                           //);

                           //var metrics = endpointConfiguration.EnableMetrics();
                           //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));


                           var azStorage = context.Configuration.GetConnectionString("AzureStorage");
                           var endpointName = "thanhnm-74DE141E26BC4449BC9A47CC8CFFB416-Sales";
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
