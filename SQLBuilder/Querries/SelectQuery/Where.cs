using System.Runtime.CompilerServices;
using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class SWhere : SelectQueryModel, ISelectable
    {
        public SWhere(SelectQueryModel queryModel) : base(queryModel) { }
        public Select<T> Select<T>(T tuple) where T : ITuple, new() 
        {
            QSelect = tuple;
            return new Select<T>(this) { QSelect = tuple };
        }
    }
}
