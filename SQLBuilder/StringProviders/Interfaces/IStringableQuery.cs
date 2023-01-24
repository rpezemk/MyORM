namespace SQLBuilder.StringProviders.Interfaces
{
    public interface IStringableQuery
    {
        public string GetString(IQueryStringProvider stringProvider);
    }

}
