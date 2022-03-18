var builder = WebApplication.CreateBuilder(args);

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

app.MapPost("/api/orders",
    async (Order order) =>
    {
        Console.WriteLine($"Order {order.Name} created");
        return await Task.FromResult(new { Success = true, Id = order.Id });
    });

app.MapDelete("/api/orders/{id}",
    async (Guid id) =>
    {
        Console.WriteLine($"Order {id} deleted");
        return await Task.FromResult(new { Success = true, Id = id });
    });

app.Run();

class Order
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}