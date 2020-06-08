using System.Data;

namespace TemplateAPI.DAL.Connection
{
    public interface IConnectionFactory
    {
        public IDbConnection GetConnection();
        public void CloseConnection();
    }
}
