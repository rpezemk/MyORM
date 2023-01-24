using System;
using SQLBuilder.Types.CaseWhen;
using SQLBuilder.ValueProviders.FieldProvider;

namespace SQLBuilder.ValueProviders.ValueProvider
{
    public class ValToCaseTProvider : ATupleValueProvider
    {
        /// <summary>
        /// val should be value of caseExpressionT.Val, type should be typeof(CaseExpression(T))
        /// </summary>
        /// <param name="val"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override ATupleProviderResult Manage(object val, Type type)
        {
            if(!(typeof(CaseExpression<>) == type.GetGenericTypeDefinition()))
                return base.Manage(val, type);
            var f = FieldFactory.GetCaseExpressionInstance(type, val);
            if (f != null)
                return new ValueOkResult() { ResultValue = f };
            else
                return new ValueErrorResult() { ErrorResult = $"something went wrong in{GetType().Name}" };
        }
    }


}
