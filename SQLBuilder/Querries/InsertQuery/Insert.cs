using System;
using System.Linq;
using SQLBuilder.Querries.InsertQueryNS.Interfaces;

namespace SQLBuilder.Querries.InsertQueryNS
{
    public class Insert<T> : InsertQuery, IValuesable
    {
        public Insert(InsertQueryModel insertQueryModel) : base(insertQueryModel)
        {
            if (QTable == null)
            {
                Console.WriteLine("check");
            }
        }

        public ValuesDef Values(params AField[] aFields)
        {
            QColumns = aFields.ToList();
            return new ValuesDef(this);
        }
    }



}
