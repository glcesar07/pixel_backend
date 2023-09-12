using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Database
{
    public class MainConnection
    {
        public static SqlConnection Connection(IConfiguration configuration)
        {
            SqlConnection conn = new SqlConnection(configuration.GetConnectionString("Connection"));
            conn.Open();
            return conn;
        }
    }
}
