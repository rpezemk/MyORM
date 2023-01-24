using System;
using SQLBuilder.StructureTypes.Chainable;

namespace SQLBuilder.ValueProviders.ValueProvider
{
    public abstract class ATupleValueProvider : AChainable<ATupleValueProvider>
    {
        public virtual ATupleProviderResult Manage(object val, Type type)
        {
            if (ChildProvider != null)
                return ChildProvider.Manage(val, type);
            else
                return new ValueOkResult() { ErrorResult = "no compatible converter found in chain!" };
        }
    }

}
