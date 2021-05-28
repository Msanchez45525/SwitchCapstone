using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SqlLib
{
   public class Connection
    {
        public SqlConnection SqlConn { get; set; }


        public Connection(string server, string database)
        {
            var connStr = $"server={server};database={database};trusted_connection=true;";
            SqlConn = new SqlConnection(connStr);
            SqlConn.Open();
            if(SqlConn.State != System.Data.ConnectionState.Open)
            {
                SqlConn = null;
                throw new Exception("Connection did not open");

            }
            
        }

    
    }






























}
