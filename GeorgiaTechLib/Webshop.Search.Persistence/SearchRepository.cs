using Dapper;
using System.Net.Http.Headers;
using Webshop.Catalog.Domain.AggregateRoots;
using Webshop.Data.Persistence;

namespace Webshop.Search.Persistence
{
    public class SearchRepository : BaseRepository, ISearchRepository
    {
        public SearchRepository(DataContext context) : base(TableNames.Search.SEARCHTABLE, context) { }

        public async Task<IAsyncResult> AddProductToCategory(int productId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DemoSearchObject>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<DemoSearchObject> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Product entity)
        {
            //TODO convert to demosearchobject

            using (var connection = dataContext.CreateConnection())
            {
                string command = $"insert into {TableName} (Name, SKU, Price, Currency, Description, AmountInStock, MinStock) values (@name, @sku, @price, @currency, @description, @stock, @minstock)";
                await connection.ExecuteAsync(command, new
                {
                    //name = entity.Name,
                    //sku = entity.SKU,
                    //price = entity.Price,
                    //currency = entity.Currency,
                    //description = entity.Description,
                    //stock = entity.AmountInStock,
                    //minstock = entity.MinStock
                });

            }
        }

        public async Task<IAsyncResult> RemoveProductFromCategory(int productId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(DemoSearchObject entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DemoSearchObject>> GetAllFromCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
