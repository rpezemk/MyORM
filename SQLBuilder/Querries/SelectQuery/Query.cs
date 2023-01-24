using System;
using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class SelectQuery : SelectQueryModel, IFromable
    { 
        public SelectQuery() : base(new SelectQueryModel())
        {
        }

        public From From<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            QFrom = table;
            return new From(this);
        }
    }
}
