using System.Data.SqlClient;

namespace SQLBuilder.Querries.DeleteQueryNS.Interfaces
{
    public interface IDCommitable { public int Commit(SqlConnection sqlConnection); }

}
