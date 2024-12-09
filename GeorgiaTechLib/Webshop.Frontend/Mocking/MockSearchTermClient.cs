using Webshop.Search.Domain;
using Webshop.Tools.APIAccess;

namespace Webshop.Frontend.Mocking
{
    public class MockSearchTermClient : ISearchServiceClient<SearchTerm, SearchResult[]>
    {

        private List<SearchResult> searchResults = new List<SearchResult>
        {
            new SearchResult(1, "Ready Player One", 2, "Science Fiction", 2011, 14.99m),
            new SearchResult(2, "Star Wars", 2, "Science Fiction", 1977, 19.99m),
            new SearchResult(3, "Lord of the Rings", 4, "Fantasy", 1954, 24.99m),
            new SearchResult(4, "Six of Crows", 4, "Fantasy", 2015, 12.99m),
            new SearchResult(5, "Gravity's Rainbow", 3, "Postmodern Fiction", 1973, 16.49m),
            new SearchResult(6, "The Crying of Lot 49", 3, "Postmodern Fiction", 1966, 9.99m)
        };

        public async Task<SearchResult[]> Post(string endpoint, SearchTerm Payload)
        {
            if (Payload.SearchType == "SearchResult")
            {
                return [.. searchResults.FindAll(x => x.Title.ToLower().Contains(Payload.Term.ToLower()))];
            }
            else if (Payload.SearchType == "Category")
            {
                return [.. searchResults.FindAll(x => x.Category == Payload.Term)];
            }

            return [];
        }
    }
}
