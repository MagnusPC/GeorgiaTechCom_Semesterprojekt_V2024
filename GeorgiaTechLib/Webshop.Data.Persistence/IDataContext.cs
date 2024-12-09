using System.Data;

namespace Webshop.Data.Persistence
{
    public interface IDataContext
    {
        public IDbConnection CreateConnection();
    }
}
