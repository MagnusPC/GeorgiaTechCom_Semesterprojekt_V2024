using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Webshop.Tools.Messaging.Producers
{
    public class Producer : Connection, IProducer
    {
        public Producer(string _queue, string _hostname = "localhost") : base(_queue, _hostname)
        {
        }

        public async Task SendMessage<T>(Message<T> message)
        {
            if (channel != null)
            {
                string json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
