using Webshop.Domain.Common;


namespace Webshop.Search.Domain
{
    public class SearchResult : AggregateRoot
    {
        public SearchResult(int bookId, string title, int categoryId, string category, int publishedYear, decimal price)
        {
            BookId = bookId;
            Title = title;
            CategoryId = categoryId;
            Category = category;
            PublishedYear = publishedYear;
            Price = price;
        }

        public SearchResult() { } // For ORM

        public int BookId { get; private set; } // Maps to BookId in the table
        public string Title { get; private set; } // Maps to Title in the table
        public int CategoryId { get; private set; } // Maps to CategoryId in the table
        public string Category { get; private set; } // Maps to Category in the table
        public int PublishedYear { get; private set; } // Maps to PublishedYear in the table
        public decimal Price { get; private set; } // Maps to Price in the table
    }
}


