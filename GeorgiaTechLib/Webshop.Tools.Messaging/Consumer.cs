using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Webshop.Tools.Messaging
{
    public class Consumer<T> : Connection
    {
        readonly Action<Message<T>> onConsume;
        public Consumer(Action<Message<T>> _onConsume, string _queue, string _hostname = "localhost") : base(_queue, _hostname)
        {
            onConsume = _onConsume;
        }

        public override async Task Connect(bool durable = false, bool exclusive = false, bool autoDelete = false)
        {
            await base.Connect(durable, exclusive, autoDelete);

            if (channel != null)
            {
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
