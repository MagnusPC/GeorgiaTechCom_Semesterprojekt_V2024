using Webshop.Tools.APIAccess;
using Webshop.Tools.TempSearchLib;

namespace Webshop.Frontend.Mocking
{
    public class MockSearchTermClient : ISearchServiceClient<SearchTerm, TempSearchObject[]>
    {
        List<TempSearchObject> tsObj = [
            new TempSearchObject(1, "Ready Player One", "Ernest Cline", 1, "Sci-Fi", 2011),
            new TempSearchObject(2, "Star Wars", "George Lucas", 1, "Sci-Fi", 1977),
            new TempSearchObject(3, "Lord of the Rings", "J.R.R. Tolkien", 2, "Fantasy", 1954),
            new TempSearchObject(4, "Six of Crows", "Leigh Bardugo", 2, "Fantasy", 2015),
            new TempSearchObject(5, "Gravity's Rainbow", "Thomas Pynchon", 3, "Sci-Fi", 1973),
            new TempSearchObject(6, "The Crying of Lot 49", "Thomas Pynchon", 3, "Sci-Fi", 1966)
            ];

        public async Task<TempSearchObject[]> Post(string endpoint, SearchTerm Payload)
        {
            if (Payload.SearchType == "Book")
            {
                return [.. tsObj.FindAll(x => x.Title.ToLower().Contains(Payload.Term.ToLower()))];
            }
            else if (Payload.SearchType == "Category")
            {
                return [.. tsObj.FindAll(x => x.Category == Payload.Term)];
            }

            return [];
        }
    }
}
