var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPut("/api/inventory/{id}", async (Guid id) =>
{
    try
    {
        throw new NotImplementedException();
        Console.WriteLine($"Product {id} updated");
        return await Task.FromResult(new { Success = true, Id = id });
    }catch (Exception ex)
    {
        return await Task.FromResult(new { Success = false, Id = id });
    }
});

app.MapDelete("/api/inventory/{id}", async (Guid id) =>
{
    Console.WriteLine($"Product {id} deleted");
    return await Task.FromResult(new { Success = true, Id = id });
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}