using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evtiop.DAL.Core
{
    public class SqlHelper
    {
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void CloseConnection(SqlConnection connection)
        {

        }
    }
}
