using Microsoft.EntityFrameworkCore;
using UploadLargeFile.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AppDbContext>();
//builder.Services.AddDbContext<AppDbContext>(config =>
//{
//    //config.UseSqlServer("Data Source=localhost;Initial Catalog=Demo;User Id=sa;Password=123456789x@X;");
//    config.UseSqlServer($"Server=localhost;Database=Demo;User ID=sa;Password=123456789x@X;TrustServerCertificate=True");
//});

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
