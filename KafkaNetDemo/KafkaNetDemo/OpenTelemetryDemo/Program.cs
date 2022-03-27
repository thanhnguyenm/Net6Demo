using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserProvider>(new UserProvider("Data Source=LAPDELL-0209;Initial Catalog=ttq-local-sqldb;User Id=sa;Password=123456789x@X;"));

string serviceName = "DemoOpenTelemetry";
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.AddConsoleExporter()
    .AddSource("serviceName")
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: "1.0.0"))
    .AddAspNetCoreInstrumentation()
    .AddSqlClientInstrumentation()
    .AddHttpClientInstrumentation();
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
