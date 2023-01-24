using SQLBuilder.Querries.SelectQueryNS;

namespace SQLBuilder.Querries.SelQueryModel.Interfaces
{
    public interface IJoinable
    {
        public LeftJoin Join<T>(out T table, string alias = "") where T : ATable, new();
        public LeftJoin LeftJoin<T>(out T table, string alias = "") where T : ATable, new();
        public InnerJoin InnerJoin<T>(out T table, string alias = "") where T : ATable, new();
        public RightJoin RightJoin<T>(out T table, string alias = "") where T : ATable, new();
        public FullOuterJoin OuterJoin<T>(out T table, string alias = "") where T : ATable, new();
    }
}
