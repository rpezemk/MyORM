using SQLBuilder.Helpers;
using SQLBuilder.StringProviders.ForQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class Runner<T> : SelectQueryModel where T : ITuple, new()
    {
        public List<T> ResultRows = new List<T>();

        public Runner(SelectQueryModel queryModel) : base(queryModel) { }

        public void Run(SqlConnection sqlConnection)
        {
            AliasHelper.MakeCorrectAliases(QJoins.Select(j => j.Table).Union(new List<ATable>() { QFrom }).ToList());
            var query = base.GetString(new GeneralQueryStringProvider());
            Console.WriteLine(query);
            var dt = new DataTable();
            dt = SQLHelpers.RunQueryToDataTable(sqlConnection, query);
            var isTuple = typeof(ITuple).IsAssignableFrom(typeof(T));
            var genericArgs = TupleHelpers.UnfoldGenericArgs(typeof(T).GetGenericArguments());
            var unfold1 = TupleHelpers.UnfoldTuple(new T());
            var testType = typeof(T);
            var numVector = Enumerable.Range(0, unfold1.Length).ToArray();
            var time1 = DateTime.Now;
            if (isTuple && QSelect != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var inputRow = numVector.Select(i => (row[i], genericArgs[i])).ToList();
                    var newTuple = (T)TupleHelpers.GetTuple(inputRow);
                    ResultRows.Add(newTuple);
                }
            }
            var time2 = DateTime.Now;
            var diff = (time2 - time1).TotalMilliseconds;
            return;
        }

    }
}
