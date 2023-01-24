using System.Runtime.CompilerServices;
using SQLBuilder.Querries.SelQueryModel.Interfaces;

namespace SQLBuilder.Querries.SelectQueryNS
{
    public class Select<T> : SelectQueryModel, IRunnable<T> where T : ITuple, new()
    {
        public Select(SelectQueryModel queryModel) : base(queryModel) { }

        public Runner<T> GetRunner()
        {
            return new Runner<T>(this as SelectQueryModel);
        }
    }
}
