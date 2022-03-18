using MassTransit;
using MediaR.MassTransit.Consumers;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//MassTransit
builder.Services.AddMediator(config =>
{
    //config.AddConsumer<GetWeatherForcastConsumer>();
    config.AddConsumers(Assembly.GetExecutingAssembly());

    config.AddRequestClient<IMessage>();
    //config.AddRequestClient<GetWeatherForcastsMessage>();
});

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
