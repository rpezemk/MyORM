using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableToClassConverter
{
    internal static class SQL
    {
        public static DataTable RunQueryToDataTable(SqlConnection sqlConn, string query)
        {

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

    }
}
