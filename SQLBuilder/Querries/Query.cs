using SQLBuilder.Querries.BaseInterfaces;
using SQLBuilder.Querries.DeleteQueryNS;
using SQLBuilder.Querries.InsertQueryNS;
using SQLBuilder.Querries.SelectQueryNS;
using SQLBuilder.Querries.UpdateQueryNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Querries
{
    internal class Query : IQuery
    {
        public Delete DeleteFrom<T>(out T table) where T : ATable, new()
        {
            table = new T();
            var deleteQuery = new DeleteQuery() { QTable = table };
            return new Delete(deleteQuery);
        }

        public Insert<T> Insert<T>() where T : ATable, new()
        {
            var table = new T();
            InsertQuery insertQuery = new InsertQuery() { QTable = table };
            return new Insert<T>(insertQuery);
        }

        public From From<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            (table as ATable).Alias = alias;
            SelectQuery selectQuery = new SelectQuery() { QFrom = table };
            return new From(selectQuery);
        }

        public Update Update<T>(out T table) where T : ATable, new()
        {
            table = new T();
            UpdateQuery updateQuery = new UpdateQuery() { QTable = table };
            return new Update(updateQuery);
        }
    }
}
