using Webshop.Application.Contracts.Persistence;
using Webshop.Domain.Common;


namespace Webshop.Search.Persistence.Contracts
{
    internal interface ISearchRepository : IRepository<Domain.SearchResult> //SearchTerm?
    {
        // R for search enginge
        Task<IEnumerable<Domain.SearchResult>> GetFromTextInput(string searchtext);

        Task<IEnumerable<Domain.SearchResult>> GetAllFromCategory(int categoryID);


    }
}
