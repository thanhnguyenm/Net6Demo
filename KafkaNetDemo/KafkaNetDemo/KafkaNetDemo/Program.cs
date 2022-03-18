// See https://aka.ms/new-console-template for more information



using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<KafkaConsumertHostingService>();
        //services.AddHostedService<KafkaProducerHostingService>();
    }).Build().Run();
