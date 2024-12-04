using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data.Persistence
{
    public class TempBaseRepository<DataContext>
    {
        public TempBaseRepository(string tableName, DataContext dataContext)
        {
            this.dataContext = dataContext;
            this.TableName = tableName;
        }

        protected string TableName { get; private set; }
        protected DataContext dataContext { get; private set; }

    }
}