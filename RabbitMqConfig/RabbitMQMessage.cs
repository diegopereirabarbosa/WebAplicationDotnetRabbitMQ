using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqConfig
{
    public class RabbitMQMessage
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Message { get; set; }
    }
}
