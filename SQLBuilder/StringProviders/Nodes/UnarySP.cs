using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for Unary
    /// </summary>
    internal class UnarySP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (typeof(Unary).IsAssignableFrom(t))
            {
                var unary = condition as Unary;

                var res = RootProvider.Manage(unary.Val);
                if (res.IsError)
                    return res;
                return new SPResultOK() { ResultValue = $"( {unary.Keyword} {res.ResultValue} )" };
            }
            else
                return base.Manage(condition);
        }
    }

}
