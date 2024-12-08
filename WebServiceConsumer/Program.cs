using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMqConfig;
using WebServiceConsumer.Consumer;
using WebServiceConsumer.Models;
using WebServiceConsumer.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMQ"));
builder.Services.AddHostedService<OrderConsumer>();
builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();


builder.Services.AddDbContextPool<GestaoVendasContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
