using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMqConfig;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text;
using System.Threading.Channels;
using WebServiceConsumer.Services;

namespace WebServiceConsumer.Consumer
{
    public class OrderConsumer: BackgroundService
    {
        private readonly RabbitMQConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
 
        public OrderConsumer(IOptions<RabbitMQConfiguration> options, IServiceProvider serviceProvider = null)
        {

            _config = options.Value;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory { HostName = _config.Host };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(_config.Queue, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                CreateOrder(message);
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(_config.Queue, autoAck: true, consumer: consumer);
        }

        protected void CreateOrder(string message)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var createOrderService = scope.ServiceProvider.GetRequiredService<ICreateOrderService>();
                createOrderService.CreateOrder(1,2, message);
            }
        }
    }
}
