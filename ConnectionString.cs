using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHA256Login
{
    public static class ConnectionString
    {
        public static SqlConnection connection() 
        {
            SqlConnection conn = new SqlConnection("Data Source=CEYHUNKUTAHYALI\\SQLEXPRESS;Initial Catalog=Project_db;Integrated Security=True");
            conn.Open();
            return conn;
        }
    }
}
