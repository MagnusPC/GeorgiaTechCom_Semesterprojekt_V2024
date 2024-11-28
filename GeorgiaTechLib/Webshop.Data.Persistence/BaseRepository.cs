using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data.Persistence
{
    public class BaseRepository<IDataContext>
    {
        public BaseRepository(string tableName, IDataContext dataContext)
        {
            this.dataContext = dataContext;
            this.TableName = tableName;
        }

        protected string TableName { get; private set; }
        protected IDataContext dataContext { get; private set; }
    }
}
