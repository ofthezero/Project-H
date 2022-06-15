using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel
{
    internal class Connn
    {
       
        protected SqlConnection  getConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = IAMROBERT\MSSQLSERVERR; Initial Catalog = Hotel; Integrated Security = True";
            return conn;
        }

        public SqlDataReader getForCombo(String query)
        {
            SqlConnection conn = getConnection();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Connection = conn;
            conn.Open();
            cmd = new SqlCommand(query, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            return sdr;
        }
    }
}
