using Ascendance.Middlewares;
using MessageHandlerAPIs;
using MessageHandlerAPIs.Contracts;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using TextHandling.Api.Data;
using TextHandling.Api.Services;
using TextHandling.Api.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TextHandlingDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataHandler,DataHandler>();
builder.Services.AddScoped < IConnectionFactory>(cF =>
    new ConnectionFactory()
    {
        VirtualHost= "localhost",
    });
builder.Services.AddScoped<IMQPublisher, MQPublisher>();
builder.Services.AddScoped<IMQConsumer, MQConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<KeyValidationMiddleware>();

app.MapControllers();

app.Run();
