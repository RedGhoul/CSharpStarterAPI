using System.Data;

namespace Persistence.Connection
{
    public interface IConnectionFactory
    {
        public IDbConnection GetConnection();
        public void CloseConnection();
    }
}
