using Webshop.Tools.APIAccess;

namespace Webshop.Frontend.Mocking
{
    public class MockSearchTermClient : ISearchServiceClient<SearchTerm, Book[]>
    {
        List<Book> books = [
            new Book() { Category = "Sci-Fi", Name = "Ready Player One", Price = 99.99f },
            new Book() { Category = "Sci-Fi", Name = "Star Wars", Price = 199.99f },
            new Book() { Category = "Fantasy", Name = "Lord of the Rings", Price = 999.99f },
            new Book() { Category = "Fantasy", Name = "Six of Crows", Price = 119.99f }
            ];

        public async Task<Book[]> Post(string endpoint, SearchTerm Payload)
        {
            if (Payload.SearchType == "Book")
            {
                return [.. books.FindAll(x => x.Name.ToLower().Contains(Payload.Term.ToLower()))];
            }
            else if (Payload.SearchType == "Category")
            {
                return [.. books.FindAll(x => x.Category == Payload.Term)];
            }

            return [];
        }
    }
}
