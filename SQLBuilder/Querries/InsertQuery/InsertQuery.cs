using SQLBuilder.Querries.InsertQueryNS.Interfaces;

namespace SQLBuilder.Querries.InsertQueryNS
{
    public class InsertQuery : InsertQueryModel, IInsertable
    {
        public InsertQuery() { }
        public InsertQuery(InsertQueryModel insertQueryModel)
        {
            QTable = insertQueryModel.QTable;
            QColumns = insertQueryModel.QColumns;
            QValues = insertQueryModel.QValues;
        }

        public Insert<T> InsertInto<T>(out T tableRow) where T : ATable, new()
        {
            tableRow = new T();
            return new Insert<T>(this) { QTable = tableRow };
        }

        public Insert<T> InsertInto<T>() where T : ATable, new()
        {
            var tableRow = new T();
            QTable = tableRow;
            return new Insert<T>(this) { QTable = tableRow };
        }
    }



}
