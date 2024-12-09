using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Webshop.Tools.Messaging.Consumers
{
    public class Consumer<T> : Connection, IConsumer
    {
        readonly Action<Message<T>> onConsume;
        public Consumer(Action<Message<T>> _onConsume, string _queue, string _hostname = "localhost") : base(_queue, _hostname)
        {
            onConsume = _onConsume;
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

                Console.WriteLine(" [*] Waiting for messages.");

                AsyncEventingBasicConsumer consumer = new(channel);
                consumer.ReceivedAsync += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);

                    Message<T> parsedMessage = JsonSerializer.Deserialize<Message<T>>(message) ?? throw new Exception("Message couldn't be parsed");
                    onConsume(parsedMessage);

                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync(queue, autoAck: true, consumer: consumer);
            }
        }
    }
}
