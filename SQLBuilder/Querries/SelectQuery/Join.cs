using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public abstract class Join : SelectQueryModel, IOnable
    {
        internal string AdditionalWord = "";
        internal ATable Table;
        internal ACondition Condition;
        public Join(SelectQueryModel queryModel) : base(queryModel){}
        public On On(ACondition condition)
        {
            Condition = condition;
            QJoins.Add(this);
            return new On(this);
        }
    }
}
