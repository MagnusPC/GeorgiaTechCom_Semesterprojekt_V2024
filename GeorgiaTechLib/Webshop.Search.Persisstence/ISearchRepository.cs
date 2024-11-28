using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Application.Contracts.Persistence;
using Webshop.Catalog.Domain.AggregateRoots;
using Webshop.Domain.Common;

namespace Webshop.Search.Persistence
{
    internal interface ISearchRepository : IRepository<Product> //SearchTerm?
    {
        Task<IEnumerable<Product>> GetAllFromCategory(int categoryId);


    }
}
