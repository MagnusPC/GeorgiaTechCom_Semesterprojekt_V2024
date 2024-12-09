
using RabbitMQ.Client;

namespace Webshop.Tools.Messaging.Producers
{
    public class Publisher : Producer
    {
        public Publisher(string _exchange, string _hostname = "localhost") : base(string.Empty, _exchange, _hostname)
        {
        }

        protected override async Task DeclareQueue(bool durable = false, bool exclusive = false, bool autoDelete = false)
        {
            if (channel != null)
            {
                await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Fanout);
            }
        }
    }
}
