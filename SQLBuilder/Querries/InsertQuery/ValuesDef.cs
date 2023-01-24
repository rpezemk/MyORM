using SQLBuilder.Querries.InsertQueryNS.Interfaces;
using SQLBuilder.StringProviders.ForQuery;
using System;
using System.Data.SqlClient;

namespace SQLBuilder.Querries.InsertQueryNS
{
    public class ValuesDef : InsertQuery, ICommitable
    {
        public ValuesDef(InsertQueryModel insertQueryModel) : base(insertQueryModel) { }
        public int Commit(SqlConnection sqlConnection)
        {
            var query = base.GetString(new GeneralQueryStringProvider());
            Console.WriteLine(query);
            var scopeId = 0;
            //scopeId = SQL.ExecuteScalar(sqlConnection, query);
            return scopeId;
        }

    }



}
