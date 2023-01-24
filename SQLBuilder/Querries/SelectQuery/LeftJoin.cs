namespace SQLBuilder.Querries.SelectQueryNS
{
    public class LeftJoin : Join
    {
        public LeftJoin(SelectQueryModel queryModel) : base(queryModel){ AdditionalWord = "LEFT"; }
    }
}
