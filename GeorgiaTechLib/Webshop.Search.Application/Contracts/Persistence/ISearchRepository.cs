using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application.Contracts.Persistence;
using Webshop.Search.Domain;
using Webshop.Domain.Common;


namespace Webshop.Search.Contracts.Persistence
{
    internal interface ISearchRepository : IRepository<Domain.SearchResult> //SearchTerm?
    {
        // R for seach engingw
        Task<IEnumerable<Domain.SearchResult>> GetFromTextInput(string searchtext);

        Task<IEnumerable<Domain.SearchResult>> GetAllFromCatID(int categoryID);

        // CUD for sync data sunc



    }
}
