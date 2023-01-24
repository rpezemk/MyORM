using System;
using System.Data.SqlClient;
using SQLBuilder.Querries.DeleteQueryNS.Interfaces;
using SQLBuilder.StringProviders.ForQuery;

namespace SQLBuilder.Querries.DeleteQueryNS
{
    public class DWhere : DeleteQueryModel, IDCommitable
    {
        public DWhere(DeleteQueryModel deleteQueryModel) : base(deleteQueryModel) { }
        public int Commit(SqlConnection sqlConnection)
        {
            var query = base.GetString(new GeneralQueryStringProvider());
            Console.WriteLine(query);
            var noOfRowsAffected = 0;
            // noOfRowsAffected = SQL.ExecuteNonQuery(sqlConnection, query);
            return noOfRowsAffected;
        }
    }

}
