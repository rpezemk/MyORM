using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder
{

    internal class SQLHelpers
    {

        public static DataTable RunQueryToDataTable(SqlConnection sqlConn, string query)
        {
            Console.WriteLine(query);
            var dt = new DataTable();
            try
            {
                using (sqlConn)
                {
                    sqlConn.Open();
                    using (SqlDataAdapter sqlCommand = new SqlDataAdapter(query, sqlConn))
                    {
                        sqlCommand.Fill(dt);
                    }
                }
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
            return dt;
        }

        public static int ExecuteScalar(SqlConnection sqlConn, string query)
        {

            int id = 0;
            try
            {
                using (sqlConn)
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConn))
                    {
                        var obj = sqlCommand.ExecuteScalar();
                        if (obj is Int32)
                            id = Convert.ToInt32(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public static int ExecuteNonQuery(SqlConnection sqlConn, string query)
        {

            int noOfRowsAffected = 0;
            try
            {
                using (sqlConn)
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConn))
                    {
                        noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return noOfRowsAffected;
        }


    }
}
