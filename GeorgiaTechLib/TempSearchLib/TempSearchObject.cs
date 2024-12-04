using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Tools.TempSearchLib
{
    internal class TempSearchObject
    {
        public int BookId { get; internal set; } // Maps to BookId in the table
        public string Title { get; internal set; } // Maps to Title in the table
        public string? Author { get; internal set; } // Maps to Author in the table
        public int CategoryId { get; internal set; } // Maps to Categoryid in the table
        public string Category { get; internal set; } // Maps to Categorty in the table
        public int PublishedYear { get; internal set; } // Maps to PublishedYear in the table
    }
}
