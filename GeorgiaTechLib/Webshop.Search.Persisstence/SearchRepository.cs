using Dapper;
using Microsoft.Extensions.Logging;
using Webshop.Application.Contracts.Persistence;
using Webshop.Data.Persistence;
using Webshop.Domain.Common;
using Webshop.Search.Domain;
using Webshop.Search.Persistence.Contracts;

namespace Webshop.Search.Persisstence
{
    public class SearchRepository : BaseRepository<PGDataContext>, ISearchRepository
    {
        private readonly ILogger<SearchRepository> _logger;
        public SearchRepository(PGDataContext context, ILogger<SearchRepository> logger) : base(TableNames.Search.SEARCHTABLE, context)
        {
            this._logger = logger;
        }
        // Serch operations
        public async Task<IEnumerable<SearchResult>> GetFromTextInput(string searchtext)

        {
            try
            {
                string sql = $"SELECT * FROM {this.TableName} WHERE Title LIKE @searchText OR Author LIKE @searchText OR Category LIKE @searchText";
                using (var connection = dataContext.CreateConnection())
                {

                    var results = await connection.QueryAsync<SearchResult>(sql, new { searchText = $"%{searchtext}%" });
                    if (results != null)
                    {
                        return results;
                    }
                    else
                    {
                        return new List<Domain.SearchResult>();
                    }
                }
            }
            catch (Exception ex)
            {

                this._logger.LogCritical(ex, ex.Message);
                return new List<Domain.SearchResult>();
            }
        }



        public async Task<IEnumerable<Domain.SearchResult>> GetAllFromCategory(int categoryID)
        {
            try
            {
                // SQL query to get all search results for a specific category
                string sql = $"SELECT * FROM {this.TableName} WHERE CategoryId = @categoryId";
                using (var connection = dataContext.CreateConnection())
                {
                    // Execute the query and return the results
                    var results = await connection.QueryAsync<SearchResult>(sql, new { categoryID });
                    if (results != null)
                    {
                        return results;
                    }
                    else
                    {
                        return new List<Domain.SearchResult>();
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, ex.Message);
                return new List<Domain.SearchResult>();
            }
        }

        // Basic CRUD operations
        public async Task CreateAsync(Domain.SearchResult entity)
        {

            try
            {
                string sql = $@"
            INSERT INTO {this.TableName} 
            (BookId, Title, Author, CategoryId, Category, PublishedYear) 
            VALUES (@BookId, @Title, @Author, @CategoryId, @Category, @PublishedYear)";

                using (var connection = dataContext.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        BookId = entity.BookId,
                        Title = entity.Title,
                        Author = entity.Author,
                        CategoryId = entity.CategoryId,
                        Category = entity.Category,
                        PublishedYear = entity.PublishedYear
                    });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, ex.Message);
                throw new Exception("Error in inserting entry into search database", ex);
            }


        }



        async Task<SearchResult> IRepository<SearchResult>.GetById(int id)
        {
            try
            {
                string sql = $"select * from {this.TableName} where BookId = @id";
                using (var connection = dataContext.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<Domain.SearchResult>(sql, new { id = id });
                    if (result != null)
                    {
                        return Result.Ok<Domain.SearchResult>(result);
                    }
                    else
                    {
                        return Result.Fail<Domain.SearchResult>(Errors.General.NotFound<int>(id));
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, ex.Message);
                return Result.Fail<Domain.SearchResult>(Errors.General.FromException(ex));
            }

        }

        public async Task<IEnumerable<Domain.SearchResult>> GetAll()
        {
            try
            {
                string sql = $"select * from {this.TableName}";
                using (var connection = dataContext.CreateConnection())
                {
                    var result = await connection.QueryAsync<Domain.SearchResult>(sql);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        return new List<Domain.SearchResult>();
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, ex.Message);
                return new List<Domain.SearchResult>();
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                string sql = $"delete from {this.TableName} where BookId = @id";
                using (var connection = dataContext.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, new { id = id });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, ex.Message);
            }
        }




        public async Task UpdateAsync(Domain.SearchResult entity)
        {
            try
            {
                // Update the search result with the provided entity properties
                string sql = $@"
            UPDATE {this.TableName} 
            SET 
                Title = @Title,
                Author = @Author,
                CategoryId = @CategoryId,
                Category = @Category,
                PublishedYear = @PublishedYear
            WHERE BookId = @BookId";

                using (var connection = dataContext.CreateConnection())
                {
                    // Execute the SQL update command
                    await connection.ExecuteAsync(sql, new
                    {
                        BookId = entity.BookId,
                        Title = entity.Title,
                        Author = entity.Author,
                        CategoryId = entity.CategoryId,
                        Category = entity.Category,
                        PublishedYear = entity.PublishedYear
                    });
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs
                this._logger.LogCritical(ex, ex.Message);
                throw new Exception("Error in updating search result in the database", ex);
            }
        }



        

    }




}
