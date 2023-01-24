namespace SQLBuilder.Helpers
{
    internal interface IFieldValueConverter
    {
        public string ConvertToSqlString(object o);
    }


}
