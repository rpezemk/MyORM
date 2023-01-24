namespace SQLBuilder.Querries.InsertQueryNS.Interfaces
{
    public interface IValuesable
    {
        /// <summary>
        /// put here fields with set values
        /// </summary>
        /// <param name="aFields"></param>
        /// <returns></returns>
        public ValuesDef Values(params AField[] aFields);
    }



}
