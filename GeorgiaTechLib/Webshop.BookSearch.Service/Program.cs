using Webshop.Tools.Messaging;



namespace Webshop.BookSearch.Service
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }


    internal class Program
    {
        static async Task Main(string[] args)
        {

            string queueName = "PostSearch";

            List<Book> books = [
            new Book() { Category = "Sci-Fi", Title = "Ready Player One", ISBN = "1234123" },
            new Book() { Category = "Sci-Fi", Title = "Star Wars", ISBN = "1234127" },
            new Book() { Category = "Fantasy", Title = "Lord of the Rings", ISBN = "2234123" },
            new Book() { Category = "Fantasy", Title = "Six of Crows", ISBN = "1234423" },
            new Book() { Category = "Sci-Fi", Title = "The future of Gaming", ISBN = "1234163" }
            ];

            Producer producer = new Producer("SearchComplete");
            await producer.Connect();

            Consumer<Book> consumer = new Consumer<Book>(
                _onConsume: async message =>
                {
                    Console.WriteLine($" [x] Received: {message.Content} with ID: {message.CorrelationId}");
                    await producer.SendMessage(new Message<List<Book>>("", books, message.CorrelationId));
                },
                _queue: queueName
            );

            


            await consumer.Connect();


            

            Console.ReadLine();
        }




    }
}
