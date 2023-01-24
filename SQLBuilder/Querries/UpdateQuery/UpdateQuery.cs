namespace SQLBuilder.Querries.UpdateQueryNS
{
    public class UpdateQuery : UpdateQueryModel, IUpdateable
    {
        public Update Update<T>(out T table) where T : ATable, new()
        {
            table = new T();
            return new Update() { QTable = table };
        }
    }
}
