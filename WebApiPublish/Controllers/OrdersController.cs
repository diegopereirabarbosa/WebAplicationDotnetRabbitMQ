using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMqConfig;
using WebApiSendOrder.Models;

namespace WebApiSendOrder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQConfiguration _config;

        public OrdersController(IOptions<RabbitMQConfiguration> options)
        {
            _config = options.Value;
            _factory = new ConnectionFactory { HostName = _config.Host };
        }

        [HttpPost]
        public async Task<IActionResult> PostOrders(Pedido order)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                queue: _config.Queue, 
                durable: false, 
                exclusive: false, 
                autoDelete: false,
                arguments: null
                );
         string orderString =   JsonConvert.SerializeObject(order);
            var body = Encoding.UTF8.GetBytes(orderString);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _config.Queue, body: body);

            return Ok();
        }
    }

}
