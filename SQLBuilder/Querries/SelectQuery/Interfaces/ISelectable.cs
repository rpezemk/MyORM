using System.Runtime.CompilerServices;
using SQLBuilder.Querries.SelectQueryNS;

namespace SQLBuilder.Querries.SelQueryModel.Interfaces
{
    public interface ISelectable { public Select<T> Select<T>(T tuple) where T : ITuple, new(); }
}
