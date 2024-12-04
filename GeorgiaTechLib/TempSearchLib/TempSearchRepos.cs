
using System.Net.Http.Headers;
using Webshop.Catalog.Domain.AggregateRoots;
using Webshop.Data.Persistence;

namespace Webshop.Tools.TempSearchLib
{

    public class TempSearchRepos : BaseRepository, ITempSearchRepos
    {

        public TempSearchRepos(DataContext context) : base(TableNames.Search.SEARCHTABLE, context) { }

        public async Task CreateAsync(Product entity)
        {
            ConvertCreateObject(entity);

            using (var connection = dataContext.CreateConnection())
            {
                string command = $"insert into {TableName} (bookId, title, author, categoryId, category, publishedYear) values (@name, @sku, @price, @currency, @description, @stock, @minstock)";
                await connection.ExecuteAsync(command, new
                {

                });
            }
        }

        private TempSearchObject ConvertCreateObject(Product entity)
        {
            TempSearchObject tsObject = new()
            {
                //tsObject.BookId = entity.SKU;
                Title = entity.Name,
                Author = "",
                CategoryId = 0,
                Category = "",
                PublishedYear = 1970
            };

            return tsObject;
        }
    }
}
