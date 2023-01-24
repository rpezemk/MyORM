using SQLBuilder.Extensions;
using SQLBuilder.Types.CaseWhen;
using System;

namespace SQLBuilder.ValueProviders.ValueProvider
{
    public class StandardValueProvider : ATupleValueProvider
    {
        public override ATupleProviderResult Manage(object val, Type type)
        {
            if (typeof(Field<>).IsAssignableFrom(type))
                return base.Manage(val, type);            
            if (typeof(CaseExpression<>).IsAssignableFrom(type))
                return base.Manage(val, type);
            if (val != null)
                return new ValueOkResult() { ResultValue = val };
            else
                return new ValueOkResult() { ResultValue = type.GetDefaultValue() };
        }
    }


}
