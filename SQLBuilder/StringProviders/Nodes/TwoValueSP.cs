using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for TwoValueCondition
    /// </summary>
    internal class TwoValueSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (typeof(TwoValueCondition).IsAssignableFrom(t))
            {
                var aCompar = condition as TwoValueCondition;
                var kwd = aCompar.Keyword;
                var res1 = RootProvider.Manage(aCompar.Condition1);
                var res2 = RootProvider.Manage(aCompar.Condition1);

                if (res1.IsError)
                    return res1;
                if (res2.IsError)
                    return res2;

                return new SPResultOK() { ResultValue = $"( {res1.ResultValue} {kwd} {res2.ResultValue} )" };
            }
            else
                return base.Manage(condition);
        }
    }

}
