using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for Not
    /// </summary>
    internal class NotSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (t == typeof(Not))
            {
                var res = RootProvider.Manage(condition as Not);
                return new SPResultOK() { ResultValue = $"( NOT {res} )" };
            }
            else
                return base.Manage(condition);
        }
    }

}
