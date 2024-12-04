using System.Data;

namespace Webshop.Data.Persistence
{
    public interface ITempDataContext
    {
        public IDbConnection CreateConnection();
    }
}