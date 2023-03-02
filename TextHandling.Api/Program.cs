using Ascendance.Middlewares;
using Microsoft.EntityFrameworkCore;
using TextHandling.Micro.Data;
using TextHandling.Micro.Services;
using TextHandling.Micro.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TextHandlingDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//TODO add rabbitMq with config

builder.Services.AddScoped<IDataHandler,DataHandler>();

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
