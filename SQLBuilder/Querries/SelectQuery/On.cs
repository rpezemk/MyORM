using System.Runtime.CompilerServices;
using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class On : SelectQueryModel, ISelectable, IWhereable, IJoinable
    {
        public On(SelectQueryModel queryModel) : base(queryModel){}

        public InnerJoin InnerJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            return new InnerJoin(this) { Table = table };
        }

        public LeftJoin Join<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            return new LeftJoin(this) { Table = table };
        }

        public LeftJoin LeftJoin<T>(out T table, string alias = "") where T : ATable, new() 
        { 
            table=new T();
            table.Alias = alias;
            return new LeftJoin(this) { Table = table };
        }

        public FullOuterJoin OuterJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            return new FullOuterJoin(this) { Table = table };
        }

        public RightJoin RightJoin<T>(out T table, string alias = "") where T : ATable, new()
        {
            table = new T();
            table.Alias = alias;
            return new RightJoin(this) { Table = table };
        }

        public Select<T> Select<T>(T tuple) where T : ITuple, new() 
        { 
            QSelect = tuple;
            return new Select<T>(this) { QSelect = tuple };
        }
        public SWhere Where(ACondition condition) 
        {
            QWhere = condition;
            return new SWhere(this);
        }
    }
}
