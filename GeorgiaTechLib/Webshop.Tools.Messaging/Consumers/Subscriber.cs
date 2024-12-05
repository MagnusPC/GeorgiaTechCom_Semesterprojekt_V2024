using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Tools.Messaging.Consumers
{
    public class Subscriber<T> : Consumer<T>
    {
        string exchangeName;
        public Subscriber(Action<Message<T>> _onConsume, string _exchangeName, string _hostname = "localhost") : base(_onConsume, string.Empty, _hostname)
        {
            exchangeName = _exchangeName;
        }

        protected override async Task DeclareQueue(bool durable = false, bool exclusive = false, bool autoDelete = false)
        {
            if (channel != null)
            {
                await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout);

                // declare a server-named queue
                QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
                queue = queueDeclareResult.QueueName;
                await channel.QueueBindAsync(queue: queue, exchange: exchangeName, routingKey: string.Empty);
            }
        }
    }
}
