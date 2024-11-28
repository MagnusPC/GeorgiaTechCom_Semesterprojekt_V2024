using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data.Persistence
{
    public class BaseRepository
    {
        public BaseRepository(string tableName, MSDataContext dataContext)
        {
            this.dataContext = dataContext;
            this.TableName = tableName;
        }

        protected string TableName { get; private set; }
        protected MSDataContext dataContext { get; private set; }
    }
}
