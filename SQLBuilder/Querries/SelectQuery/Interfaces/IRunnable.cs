using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLBuilder.Querries.SelectQueryNS;

namespace SQLBuilder.Querries.SelQueryModel.Interfaces
{
    public interface IRunnable<T> where T : ITuple, new() { public Runner<T> GetRunner(); }
}
