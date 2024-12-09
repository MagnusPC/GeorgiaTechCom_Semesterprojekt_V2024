using Dapper;
using Webshop.Application.Contracts.Persistence;
using Webshop.Data.Persistence;
using Webshop.Domain.Common;
using Webshop.Search.Domain;
using Webshop.Search.Persistence.Contracts;

namespace Webshop.Search.Persisstence
{

    public class SearchRepository : BaseRepository<PGDataContext>, ISearchRepository
    {
        
        public SearchRepository(PGDataContext context) : base(TableNames.Search.SEARCHTABLE, context)
        {
         
        }
        // Serch operations
        public async Task<IEnumerable<SearchResult>> GetFromTextInput(string searchtext)

        {
            try
            {
                string sql = $"SELECT * FROM {this.TableName} WHERE Title ILIKE @searchText OR Category ILIKE @searchText";
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
            VALUES (@BookId, @Title, @CategoryId, @Category, @PublishedYear, @Price)";

                using (var connection = dataContext.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        BookId = entity.BookId,
                        Title = entity.Title,
                      
                        CategoryId = entity.CategoryId,
                        Category = entity.Category,
                        PublishedYear = entity.PublishedYear,
                        Price = entity.Price

                    });
                }
            }
            catch (Exception ex)
            {
                
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
                CategoryId = @CategoryId,
                Category = @Category,
                PublishedYear = @PublishedYear
                Price = @Price
            WHERE BookId = @BookId";

                using (var connection = dataContext.CreateConnection())
                {
                    // Execute the SQL update command
                    await connection.ExecuteAsync(sql, new
                    {
                        BookId = entity.BookId,
                        Title = entity.Title,
                        
                        CategoryId = entity.CategoryId,
                        Category = entity.Category,
                        PublishedYear = entity.PublishedYear,
                        Price = entity.Price
                    });
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs
               
                throw new Exception("Error in updating search result in the database", ex);
            }
        }



        

    }




}
