namespace SQLBuilder.Querries.UpdateQueryNS
{
    public interface IUpdateable { public Update Update<T>(out T table) where T : ATable, new(); }



}
