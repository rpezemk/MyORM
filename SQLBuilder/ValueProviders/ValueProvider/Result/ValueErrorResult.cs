using SQLBuilder.Helpers.TupleManagerChain.ChainResult;

namespace SQLBuilder.ValueProviders.ValueProvider
{
    public class ValueErrorResult : ATupleProviderResult
    {
        public override bool IsError => true;
    }

}
