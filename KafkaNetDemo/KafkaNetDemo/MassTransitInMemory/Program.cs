using MassTransit;
using MassTransitInMemory.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<GetWheatherForecastConsumer>();
    config.UsingInMemory((ctx, cfg) =>
    {
        cfg.ConfigureEndpoints(ctx);
    });

    //config.UsingAzureServiceBus((context, cfg) =>
    //{
    //    var connectionString = "your connection string";
    //    cfg.Host(connectionString);

    //    cfg.ConfigureEndpoints(context);
    //});

    config.AddRequestClient<IMessage>();
});

builder.Services.AddMassTransitHostedService(true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
