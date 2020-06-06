using API.Utilities.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TemplateAPI.DAL.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IDbConnection _connection;
        private readonly IConfigManager _configManager;

        public ConnectionFactory(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_configManager.GetConnectionString("PrimaryConnection"));
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
    }


}
