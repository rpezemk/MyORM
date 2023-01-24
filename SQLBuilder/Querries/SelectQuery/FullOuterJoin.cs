namespace SQLBuilder.Querries.SelectQueryNS
{
    public class FullOuterJoin : Join
    {
        public FullOuterJoin(SelectQueryModel queryModel) : base(queryModel) { AdditionalWord = "FULL OUTER"; }
    }
}
