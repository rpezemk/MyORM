using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Querries.InsertQueryNS.Interfaces
{
    /// <summary>
    /// Interace for implementing InsertInto
    /// </summary>
    public interface IInsertable
    {
        public Insert<T> InsertInto<T>() where T : ATable, new();
    }

}
