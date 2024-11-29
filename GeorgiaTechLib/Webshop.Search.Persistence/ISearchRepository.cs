using Webshop.Application.Contracts.Persistence;
using Webshop.Catalog.Domain.AggregateRoots;

namespace Webshop.Search.Persistence
{
    // More or less exact copy of IProductRepository, but decoupled from all its dependencies
    public interface ISearchRepository
    {
        //From IProductRepository
        Task<IEnumerable<DemoSearchObject>> GetAllFromCategory(int categoryId); // search type
        //TODO update/change IAsyncResult?
        Task<IAsyncResult> AddProductToCategory(int productId, int categoryId); // add to type - used when creating new products
        Task<IAsyncResult> RemoveProductFromCategory(int productId, int categoryId); // remove from type - used when updating/deleting products

        //From IRepository
        Task CreateAsync(Product entity);
        Task DeleteAsync(int id);
        Task<DemoSearchObject> GetById(int id);
        Task<IEnumerable<DemoSearchObject>> GetAll();
        Task UpdateAsync(DemoSearchObject entity);
    }
}