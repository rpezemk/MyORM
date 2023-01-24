using SQLBuilder.Querries.DeleteQueryNS;
using SQLBuilder.Querries.InsertQueryNS;
using SQLBuilder.Querries.SelectQueryNS;
using SQLBuilder.Querries.UpdateQueryNS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.StringProviders.Interfaces
{
    public interface IQueryStringProvider
    {
        public string GetString(SelectQueryModel query);
        public string GetString(InsertQueryModel query);
        public string GetString(UpdateQueryModel query);
        public string GetString(DeleteQueryModel query);
    }


}
