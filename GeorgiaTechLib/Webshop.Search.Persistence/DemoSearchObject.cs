using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Search.Persistence
{
    public class DemoSearchObject
    {
		//product and category table columns
        
		/*Name] [nvarchar](150) NOT NULL,
		[SKU] [nvarchar](50) NOT NULL,
		[Price] [int] NOT NULL,
		[Currency] [nvarchar](3) NOT NULL,
		[Description] [ntext] NULL,
		[AmountInStock] [int] NULL,
		[MinStock] [int] NULL,*/

        /*[Name] [nvarchar](150) NOT NULL,
		[ParentId] [int] NOT NULL,
		[Description] [ntext] NOT NULL,*/

		public int SearchProductID { get; set; }
		public string SearchProductName { get; set; }
		public string SearchProductType { get; set; }
		public string SearchProductDescription { get; set; }

    }
}
