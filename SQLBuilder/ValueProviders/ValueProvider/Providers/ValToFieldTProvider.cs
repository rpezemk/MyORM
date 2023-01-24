using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLBuilder.Helpers.TupleManagerChain.ChainResult;
using SQLBuilder.ValueProviders.FieldProvider;

namespace SQLBuilder.ValueProviders.ValueProvider
{


    public class ValToFieldTProvider : ATupleValueProvider
    {
        /// <summary>
        /// val should be value of field.Val, type should be typeof(Field(T))
        /// </summary>
        /// <param name="val"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override ATupleProviderResult Manage(object val, Type type)
        {
            if (!(typeof(Field<>) == type.GetGenericTypeDefinition())) 
                return base.Manage(val, type);
            var f = FieldFactory.GetFieldInstance(type, val);
            if (f != null)
                return new ValueOkResult() { ResultValue = f };
            else
                return new ValueErrorResult() { ErrorResult = $"something went wrong in{GetType().Name}" };
        }
    }


}
