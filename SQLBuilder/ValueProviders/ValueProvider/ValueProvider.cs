using SQLBuilder.Helpers.TupleManagerChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.ValueProviders.ValueProvider
{

    public class ValueProvider
    {
        private static ValueProvider instance;
        private ATupleValueProvider provider;
        public static ATupleValueProvider GetValueProviderChain()
        {
            if (instance == null)
                instance = new ValueProvider();

            if(instance.provider == null)
            {
                instance.provider =
                    new ValToFieldTProvider()
                    .SetNext(new ValToCaseTProvider())
                    .SetNext(new StandardValueProvider()).RootProvider;
            }

            return instance.provider;
        }
    }
}
