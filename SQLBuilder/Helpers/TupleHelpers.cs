using SQLBuilder.StringProviders.Chains;
using SQLBuilder.StringProviders.Nodes;
using SQLBuilder.StringProviders.ProviderChain;
using SQLBuilder.Types.CaseWhen;
using SQLBuilder.ValueProviders.FieldProvider;
using SQLBuilder.ValueProviders.ValueProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SQLBuilder
{
    public static class TupleHelpers
    {
        public static AStringProvider ProviderChain { get; set; } = new WhereSP()
                        .SetNext(new CaseSP())
                        .SetNext(new ComparableSP())
                        .SetNext(new FieldNonAliasSP())
                        .SetNext(new UnarySP())
                        .SetNext(new ExistsSP())
                        .SetNext(new TwoValueSP())
                        .SetNext(new InSP())
                        .SetNext(new NotSP())
                        .SetNext(new GeneralStringProvider()).RootProvider;


        /// <summary>
        /// unfolds generic types for nested tuple
        /// </summary>
        /// <param name="inputTypes"></param>
        /// <returns></returns>
        public static Type[] UnfoldGenericArgs(Type[] inputTypes)
        {
            var resutlTypes = new List<Type>();
            foreach (var inputType in inputTypes)
            {
                if (typeof(ITuple).IsAssignableFrom(inputType))
                {
                    var types = UnfoldGenericArgs(inputType.GetGenericArguments());
                    resutlTypes.AddRange(types);
                }
                else
                {
                    resutlTypes.Add(inputType);
                }
            }
            return resutlTypes.ToArray();
        }

        /// <summary>
        /// unfolds nested tuple values
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static object[] UnfoldTuple(ITuple tuple)
        {
            List<object> unfolded = new List<object>();

            var fields = tuple.GetType().GetFields();
            var simpleFields = fields.Take(7);
            var resValues = simpleFields.Select(f => f.GetValue(tuple)).ToList();
            unfolded.AddRange(simpleFields);
            if (fields.Count() <= 7)
                return resValues.ToArray();
            var subIsTuple = typeof(ITuple).IsAssignableFrom(fields[7].FieldType);

            if (subIsTuple)
            {
                var subTuple = fields[7].GetValue(tuple);
                var subUnfolded = UnfoldTuple(subTuple as ITuple);
                resValues.AddRange(subUnfolded);
            }
            else
            {
                resValues.Add(fields[7]);
            }
            return resValues.ToArray();
        }


        /// <summary>
        /// emits tuple form list of (value, destType). 
        /// If type is derived from AField then output 
        /// tuple has AField at according place with .Val = value.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static ITuple GetTuple(List<(object value, Type type)> values)
        {
            var quantity = values.Count();
            object instance;
            if (quantity > 7)
            {
                var passedFields = values.Skip(7).ToList();
                var subTuple = GetTuple(passedFields);
                var subTupleType = subTuple.GetType();
                Type eightValTuple = typeof(ValueTuple<,,,,,,,>);
                var fieldTypes = values.Take(7).Select(v => v.type).ToList();
                fieldTypes.Add(subTupleType);
                var genericTupleType = eightValTuple.MakeGenericType(fieldTypes.ToArray());
                instance = Activator.CreateInstance(genericTupleType);
                var c = 0;
                foreach (var field in genericTupleType.GetFields())
                {
                    var value = values[c].value;
                    if (c == 7)
                    {
                        field.SetValue(instance, subTuple);
                    }
                    else 
                    {
                        var o = ValueProvider.GetValueProviderChain().Manage(value, field.FieldType).ResultValue;
                        field.SetValue(instance, o);
                    }

                    c++;
                }

            }
            else
            {
                Type type = GetTupleByLength(quantity);

                List<Type> fieldTypes = new List<Type>();
                var resultValues = new List<object>();
                foreach (var v in values)
                {
                    var gt = v.type;
                    object o = null;
                    o = ValueProvider.GetValueProviderChain().Manage(v.value, v.type).ResultValue;
                    resultValues.Add(o);
                }

                var genericTupleType = type.MakeGenericType(values.Select(v => v.type).ToArray());
                instance = Activator.CreateInstance(genericTupleType);
                var c = 0;
                foreach (var field in instance.GetType().GetFields())
                {
                    var value = resultValues[c];
                    if (value == null)
                        continue;
                    field.SetValue(instance, value);
                    c++;
                }
            }
            return instance as ITuple;
        }

        /// <summary>
        /// returns generic type of ValueTuple<...> with specified number  generic (empty) args 
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private static Type GetTupleByLength(int quantity)
        {
            return quantity switch
            {
                1 => typeof(ValueTuple<>),
                2 => typeof(ValueTuple<,>),
                3 => typeof(ValueTuple<,,>),
                4 => typeof(ValueTuple<,,,>),
                5 => typeof(ValueTuple<,,,,>),
                6 => typeof(ValueTuple<,,,,,>),
                7 => typeof(ValueTuple<,,,,,,>),
                _ => (null)
            };
        }


        public static List<string> GetTupleFieldNames(object tupObj)
        {
            var resList = new List<string>();
            var additionalList = new List<string>();

            var thisType = tupObj.GetType();

            var len = thisType.GetFields().Count();
            var fields = thisType.GetFields();
            var props = thisType.GetProperties();

            var hasFields = fields.Count() > 0;
            var hasProps = props.Count() > 0;
            var chain = StringConvertersChain.GetAliasedProviderChainInstance();
            if (hasFields)
            {
                for (int i = 0; i < len; i++)
                {
                    var field = fields[i];
                    if (!typeof(ITuple).IsAssignableFrom(field.FieldType))
                    {
                        var val = field.GetValue(tupObj);
                        var chainRes = chain.Manage(val);
                        resList.Add(chainRes.ResultValue);
                        
                    }
                    else
                    {
                        additionalList = GetTupleFieldNames(field.GetValue(tupObj) as ITuple);
                    }
                }
            }

            resList.AddRange(additionalList);
            return resList;
        }
    }
}
