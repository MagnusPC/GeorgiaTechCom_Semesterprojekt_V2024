
using System.Net.Http.Headers;
using Webshop.Catalog.Domain.AggregateRoots;
using Webshop.Data.Persistence;

namespace Webshop.Tools.TempSearchLib
{

    public class TempSearchRepos : TempBaseRepository<TempPGDataContext>, ITempSearchRepos
    {

        public TempSearchRepos(TempPGDataContext context) : base(TableNames.Search.SEARCHTABLE, context) { }

        public async Task CreateAsync(Product entity)
        {
            TempSearchObject prodConverted = ConvertCreateObject(entity);

            using (var connection = dataContext.CreateConnection())
            {
                string command = $"INSERT INTO {TableName} (BookId, Title, Author, Categoryid, Category, PublishedYear) VALUES (@sku, @name, @author, @catId, @cat, @publishedYear)";
                //string command = $"insert into {TableName} (Name, SKU, Price, Currency, Description, AmountInStock, MinStock) values (@name, @sku, @price, @currency, @description, @stock, @minstock)";
                await connection.ExecuteAsync(command, new
                {
                    sku = prodConverted.BookId.ToString(),
                    name = prodConverted.Title,
                    author = prodConverted.Author,
                    catId = prodConverted.CategoryId,  
                    cat = prodConverted.Category,
                    publishedYear = prodConverted.PublishedYear //TODO fix these
                });
            }
        }

        private TempSearchObject ConvertCreateObject(Product entity)
        {
            TempSearchObject tsObject = new()
            {
                BookId = 0,
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
