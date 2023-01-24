using System.Linq;
using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for AIn
    /// </summary>
    internal class InSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (typeof(AIn).IsAssignableFrom(t))
            {
                var ain = condition as AIn;
                var resVal = RootProvider.Manage(ain.BaseVal);
                if (resVal.IsError)
                    return resVal;
                var resValues = ain.BaseValues.Select(bv => RootProvider.Manage(bv)).ToList();
                var defRes = resValues.Find(res => res.IsError);
                if (defRes.IsError)
                    return defRes;
                var resListStr = string.Join(", ", resValues.Select(v => v.ResultValue));
                return new SPResultOK() { ResultValue = $"( {resVal.ResultValue} IN {resListStr} )" };
            }
            else
                return base.Manage(condition);
        }
    }

}
