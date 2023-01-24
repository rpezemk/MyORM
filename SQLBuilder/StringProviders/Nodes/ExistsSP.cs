using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;
using SQLBuilder.Types.CaseWhen;
using System.Linq;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for Exists
    /// </summary>
    internal class ExistsSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (typeof(Exists) == t)
            {
                var unary = condition as Unary;
                var res = RootProvider.Manage(unary.Val);
                if (res.IsError)
                    return res;
                return new SPResultOK() { ResultValue = $" EXISTS ( {res.ResultValue} )" };
            }
            else
                return base.Manage(condition);
        }
    }

    internal class CaseSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if(t.IsGenericType == false)
                return base.Manage(condition);

            if (typeof(CaseExpression<>) == t.GetGenericTypeDefinition())
            {
                var baseCaseWhen = condition as ACaseWhen;
                var val = baseCaseWhen.BaseValue;

                var list = baseCaseWhen.SingleWhens.Select(x => $" WHEN {RootProvider.Manage(x.Condition).ResultValue} THEN {RootProvider.Manage(x.Value).ResultValue}").ToList();
                var resListStr = string.Join("\n ", list);
                
                return new SPResultOK() { ResultValue = $" CASE {resListStr} END " };
            }
            else
                return base.Manage(condition);
        }
    }

}
