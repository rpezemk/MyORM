namespace SQLBuilder.Querries.UpdateQueryNS
{
    public class Set : UpdateQueryModel, ISetable, IWhereable
    {
        public Set() {}
        public Set(UpdateQueryModel updateQueryModel) : base(updateQueryModel){}
        public AField Field;
        public object Value;
        public UWhere Where(ACondition condition)
        {
            var where = new UWhere(this) { QWhere = condition };
            return where;
        }

        public Set SetF<T>(Field<T> field, T value)
        {
            var set = new Set(this) { Field = field, Value = value };
            QSetList.Add(set);
            return set;
        }
    }



}
