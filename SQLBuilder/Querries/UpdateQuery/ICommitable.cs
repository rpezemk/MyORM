using System.Data.SqlClient;

namespace SQLBuilder.Querries.UpdateQueryNS
{
    internal interface ICommitable { public int Commit(SqlConnection sqlConnection); }
}
