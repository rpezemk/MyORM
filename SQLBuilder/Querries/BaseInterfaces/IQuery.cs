using SQLBuilder.Querries.DeleteQueryNS;
using SQLBuilder.Querries.UpdateQueryNS;
using SQLBuilder.Querries.InsertQueryNS;
using SQLBuilder.Querries.SelectQueryNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Querries.BaseInterfaces
{
    public interface IQuery
    {
        public Delete DeleteFrom<T>(out T table) where T : ATable, new();
        public Update Update<T>(out T table) where T : ATable, new();
        public Insert<T> Insert<T>() where T : ATable, new();
        public From From<T>(out T table, string alias) where T : ATable, new();
    }
}
