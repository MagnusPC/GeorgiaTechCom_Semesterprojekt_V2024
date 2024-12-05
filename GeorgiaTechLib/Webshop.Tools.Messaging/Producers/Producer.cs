using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Webshop.Tools.Messaging.Producers
{
    public class Producer : Connection, IProducer
    {
        protected string exchange;


        public Producer(string _queue, string _hostname = "localhost") : base(_queue, _hostname)
        {
            exchange = string.Empty;
        }

        protected Producer(string _queue, string _exchange, string _hostname) : base(_queue, _hostname)
        {
            exchange = _exchange;
        }

        protected virtual async Task DeclareQueue(bool durable = false, bool exclusive = false, bool autoDelete = false)
        {
            if (channel != null)
            {
                await channel.QueueDeclareAsync(queue: queue, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: null);
            }
        }

        public override async Task Connect(bool durable = false, bool exclusive = false, bool autoDelete = false)
        {
            await base.Connect(durable, exclusive, autoDelete);

            if (channel != null)
            {
                await DeclareQueue(durable, exclusive, autoDelete);
            }
        }

        public async Task SendMessage<T>(Message<T> message)
        {
            if (channel != null)
            {
                string json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: exchange, routingKey: queue, body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
