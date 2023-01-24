using SQLBuilder.StringProviders.ForQuery;
using System;
using System.Data.SqlClient;

namespace SQLBuilder.Querries.UpdateQueryNS
{
    public class UWhere : UpdateQueryModel, ICommitable
    {
        public UWhere() { }
        public UWhere(UpdateQueryModel updateQueryModel) : base(updateQueryModel) { }
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
