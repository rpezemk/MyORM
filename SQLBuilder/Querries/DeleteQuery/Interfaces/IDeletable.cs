namespace SQLBuilder.Querries.DeleteQueryNS.Interfaces
{
    public interface IDeletable { public Delete DeleteFrom<T>(out T table) where T : ATable, new(); }

}
