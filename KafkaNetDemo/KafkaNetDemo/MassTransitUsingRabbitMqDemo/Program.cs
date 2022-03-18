using MassTransit;
using MassTransit.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//var bus = Bus.Factory.CreateUsingRabbitMq(config =>
//{
//    config.Host("amqp://guest:guest@localhost:5672");
//    config.ReceiveEndpoint("temp_mass_queue", c =>
//    {
//        c.Handler<Order>(ctx =>
//        {
//            return Console.Out.WriteLineAsync($"{ctx.Message.Id}");
//        });
//    });
//});


//bus.Start();

//bus.Publish(new Order { Id = Guid.NewGuid() });

app.MapPost("/api/orders", async (Order order, IPublishEndpoint publishEndpoint) =>
{
    await publishEndpoint.Publish<Order>(order);
    return order;
});


app.Run();
