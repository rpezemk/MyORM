namespace SQLBuilder.Querries.SelectQueryNS
{
    public class RightJoin : Join
    {
        public RightJoin(SelectQueryModel queryModel) : base(queryModel) { AdditionalWord = "RIGHT"; }
    }
}
