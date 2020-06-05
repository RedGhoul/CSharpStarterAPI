using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAPI.DAL.Connection
{
    public interface IConnectionFactory
    {
        public IDbConnection GetConnection();
        public void CloseConnection();
    }
}
