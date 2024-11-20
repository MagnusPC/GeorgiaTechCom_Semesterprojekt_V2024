using RabbitMQ.Client;
using System.Text;

namespace Webshop.Tools.Messaging
{
    public class Connection
    {
        protected IConnection? connection;
        protected IChannel? channel;
        protected readonly string queue;
        protected readonly string hostname;

        public Connection(string _queue, string _hostname)
        {
            queue = _queue;
            hostname = _hostname;
        }

        public virtual async Task Connect(bool durable = false, bool exclusive = false, bool autoDelete = false)
        {
            ConnectionFactory factory = new() { HostName = hostname };
            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queue, durable: durable, exclusive: exclusive, autoDelete: autoDelete,
                arguments: null);
        }
    }
}
