using Webshop.Domain.Common;


namespace Webshop.Search.Domain
{
    public class SearchResult : AggregateRoot
    {
        public SearchResult(int bookId, string title, string author, int categoryId, string category, int publishedYear)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            CategoryId = categoryId;
            Category = category;
            PublishedYear = publishedYear;
        }

        public SearchResult() { } // For ORM

        

        public int BookId { get; private set; } // Maps to BookId in the table
        public string Title { get; private set; } // Maps to Title in the table
        public string Author { get; private set; } // Maps to Author in the table
        public int CategoryId { get; private set; } // Maps to Categoryid in the table
        public string Category { get; private set; } // Maps to Categorty in the table
        public int PublishedYear { get; private set; } // Maps to PublishedYear in the table
    }
}


