using SQLBuilder.Querries.SelectQueryNS;

namespace SQLBuilder.Querries.SelQueryModel.Interfaces
{
    public interface IFromable { public From From<T>(out T table, string alias = "") where T : ATable, new(); }
}
