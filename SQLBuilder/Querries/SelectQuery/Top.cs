using System.Runtime.CompilerServices;
using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class Top : SelectQueryModel, IJoinable, IWhereable, ISelectable
    {
        public Top(SelectQueryModel queryModel) : base(queryModel) { }

        public InnerJoin InnerJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            var join = new InnerJoin(this) { Table = table };
            return join;
        }

        public LeftJoin Join<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            var join = new LeftJoin(this) { Table = table };
            return join;
        }

        public LeftJoin LeftJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            var join = new LeftJoin(this) { Table = table };
            return join;
        }

        public FullOuterJoin OuterJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            var join = new FullOuterJoin(this) { Table = table };
            return join;
        }

        public RightJoin RightJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            var join = new RightJoin(this) { Table = table };
            return join;
        }

        public Select<T> Select<T>(T tuple) where T : ITuple, new()
        {
            var select = new Select<T>(this) { QSelect = tuple };
            return select;
        }
        public SWhere Where(ACondition condition)
        {
            return new SWhere(this) { QWhere = condition };
        }

    }
}
