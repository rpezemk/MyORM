using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Helpers
{
    public static class FieldHelpers
    {

        /// <summary>
        /// Tries to convert val to Field instance
        /// </summary>
        /// <param name="val">value</param>
        /// <param name="type">type (type of field(underlying type))</param>
        /// <param name="field">resulting Field(underlying type)</param>
        /// <returns>sdf</returns>
        public static bool TryConvertValToFieldT(object val, Type type, out AField field)
        {
            field = null;
            if (type == null)
                return false;
            if (type.IsGenericType)
                return false;

            Type constructed = typeof(Field<>).MakeGenericType(type);
            object o2 = Activator.CreateInstance(constructed);
            field = o2 as AField;
            field.BVal = val;
            return true;
        }


        public static T? FindItemOrDefault<T>(T value, string id)
        {
            return default;
        }

        public static void Test()
        {
            //FindItemOrDefault(null, "sdf");
        }

    }
}
