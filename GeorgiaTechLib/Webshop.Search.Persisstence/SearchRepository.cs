using Dapper;
using Webshop.Search.Persistence;
using Webshop.Data.Persistence;
using Webshop.Domain.Common;
using Webshop.Catalog.Domain.AggregateRoots;

namespace Webshop.Search.Persisstence
{
    public class SearchRepository : BaseRepository<PGDataContext>, ISearchRepository
    {
        public SearchRepository(PGDataContext context) : base(TableNames.Search.SEARCHTABLE, context) { }

        public Task CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            throw new NotImplementedException();
        }


        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        //public async Task<Result> GetResultsAsync(string Searchterm, string Searchtype) //something like that

        public async Task<IEnumerable<Product>> GetAllFromCategory(int categoryId)
        {
            using (var connection = dataContext.CreateConnection())
            {
                string query = $"select * from {TableName} a join {TableNames.Search.SEARCHTABLE} b on a.Id = b.ProductId where b.CategoryId = @categoryid";
                return await connection.QueryAsync<Product>(query, new { categoryId = categoryId });
            }
        }
    }
}