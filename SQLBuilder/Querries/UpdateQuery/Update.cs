namespace SQLBuilder.Querries.UpdateQueryNS
{
    public class Update : UpdateQueryModel, ISetable
    {
        public Update(){}
        public Update(UpdateQueryModel updateQueryModel) : base(updateQueryModel) { }
        public Set SetF<T>(Field<T> field, T value)
        {
            var set = new Set(this) { Field = field, Value = value };
            QSetList.Add(set);
            return set;
        }
    }



}
