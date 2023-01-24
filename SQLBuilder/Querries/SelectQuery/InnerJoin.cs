namespace SQLBuilder.Querries.SelectQueryNS
{
    public class InnerJoin : Join
    {
        public InnerJoin(SelectQueryModel queryModel) : base(queryModel) { AdditionalWord = "INNER"; }
    }
}
