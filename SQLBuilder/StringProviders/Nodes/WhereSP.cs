using SQLBuilder.Querries.SelectQueryNS;
using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for WHERE
    /// </summary>
    internal class WhereSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (t == typeof(SWhere))
            {
                var aCompar = condition as SWhere;
                var cond = aCompar.QWhere;
                var res1 = RootProvider.Manage(cond);
                if (res1.IsError)
                    return res1;

                return new SPResultOK() { ResultValue = $" WHERE {res1.ResultValue} " };
            }
            else
                return base.Manage(condition);
        }

    }

}
