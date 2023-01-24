namespace SQLBuilder.Helpers.TupleManagerChain.ChainResult
{
    public abstract class AValResult<T>
    {
        public T ResultValue { get; set; }
        public abstract bool IsError { get; }
        public string ErrorResult { get; set; }
    }


}
