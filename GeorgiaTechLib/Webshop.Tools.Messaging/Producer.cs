using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Webshop.Tools.Messaging
{
    public class Producer : Connection
    {
        public Producer(string _queue, string _hostname = "localhost") : base(_queue, _hostname)
        {
        }

        public async Task SendMessage<T>(Message<T> message)
        {
            if (channel != null)
            {
                string json = JsonSerializer.Serialize<Message<T>>(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
