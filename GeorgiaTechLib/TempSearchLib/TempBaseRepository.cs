using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data.Persistence
{
    public class TempBaseRepository<ITempDataContext>
    {
        public TempBaseRepository(string tableName, ITempDataContext dataContext)
        {
            this.dataContext = dataContext;
            this.TableName = tableName;
        }

        protected string TableName { get; private set; }
        protected ITempDataContext dataContext { get; private set; }

    }
}