using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Search.Domain
{
    public class SearchTerm
    {
        public SearchTerm()
        {
        }

        public SearchTerm(string type,string term, string category)
        {
            SearchType = type;
            Term = term;
            Category = category;
        }
        public string SearchType { get; set; }
        public string Term { get; set; }
        public string Category { get; set; }
    }
}
