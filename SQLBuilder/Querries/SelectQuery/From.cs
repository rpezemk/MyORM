using System.Runtime.CompilerServices;
using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class From : SelectQueryModel, IJoinable, IWhereable, ISelectable, IToppable
    {
        public From(SelectQueryModel queryModel) : base(queryModel){}

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

        public Select<T> Select<T>(T tuple) where T: ITuple, new() 
        {
            var select = new Select<T>(this) { QSelect = tuple, WithNolock = this.WithNolock };
            return select;
        }

        public Top Top(int top)
        {
            return new Top(this) { QTop = top };
        }

        public SWhere Where(ACondition condition) 
        { 
            return new SWhere(this) { QWhere = condition }; 
        }

        public From AllWithNolock()
        {
            WithNolock = true;
            return this;
        }

    }
}
