using System.Data.SqlClient;

namespace SQLBuilder.Querries.InsertQueryNS.Interfaces
{
    public interface ICommitable
    {
        /// <summary>
        /// Runs query and returns scope_identity()
        /// </summary>
        /// <returns></returns>
        public int Commit(SqlConnection sqlConnection);
    }



}
