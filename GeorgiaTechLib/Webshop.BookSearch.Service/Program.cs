using Webshop.Tools.Messaging;


namespace Webshop.BookSearch.Service
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            string queueName = "PostSearch";

            var consumer = new Consumer<string>(
                _onConsume: message =>
                {
                    Console.WriteLine($" [x] Received: {message.Content} with ID: {message.CorrelationId}");
                },
                _queue: queueName
            );

            await consumer.Connect();
            

        }
    }
}
