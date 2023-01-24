using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLBuilder.Querries.DeleteQueryNS.Interfaces;

namespace SQLBuilder.Querries.DeleteQueryNS
{
    public class DeleteQuery : DeleteQueryModel, IDeletable
    {
        public Delete DeleteFrom<T>(out T table) where T : ATable, new()
        {
            table = new T();
            return new Delete(this) { QTable = table };
        }
    }

}
