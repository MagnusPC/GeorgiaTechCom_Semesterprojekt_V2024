using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;

namespace Webshop.Tools.TempSearchLib
{
    public class TempSearchObject
    {
        public TempSearchObject(int bookId, string title, string author, int categoryId, string category, int publishedYear)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            CategoryId = categoryId;
            Category = category;
            PublishedYear = publishedYear;
        }
        public TempSearchObject() { }

        public int BookId { get; internal set; } // Maps to BookId in the table
        public string Title { get; internal set; } // Maps to Title in the table
        public string? Author { get; internal set; } // Maps to Author in the table
        public int CategoryId { get; internal set; } // Maps to Categoryid in the table
        public string Category { get; internal set; } // Maps to Categorty in the table
        public int PublishedYear { get; internal set; } // Maps to PublishedYear in the table
    }
}