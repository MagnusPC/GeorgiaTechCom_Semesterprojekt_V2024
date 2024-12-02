using Webshop.Search.Domain;
using Webshop.Tools.APIAccess;

namespace Webshop.Frontend.Mocking
{
    public class MockSearchTermClient : ISearchServiceClient<SearchTerm, SearchResult[]>
    {

        private List<SearchResult> searchResults = new List<SearchResult>
        {
            new SearchResult(1, "Ready Player One", "Ernest Cline", 1, "Sci-Fi", 2011),
            new SearchResult(2, "Star Wars", "George Lucas", 1, "Sci-Fi", 1977),
            new SearchResult(3, "Lord of the Rings", "J.R.R. Tolkien", 2, "Fantasy", 1954),
            new SearchResult(4, "Six of Crows", "Leigh Bardugo", 2, "Fantasy", 2015),
            new SearchResult(5, "Gravity's Rainbow", "Thomas Pynchon", 3, "Sci-Fi", 1973),
            new SearchResult(6, "The Crying of Lot 49", "Thomas Pynchon", 3, "Sci-Fi", 1966)
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
