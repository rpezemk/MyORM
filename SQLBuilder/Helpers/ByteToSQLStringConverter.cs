using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace SQLBuilder.Helpers
{
    internal class ByteToSQLStringConverter : IFieldValueConverter
    {
        public string ConvertToSqlString(object o)
        {
            var res = "NULL";
            if (o == null)
                return res;
            if (!(o is byte[]))
                return res;
            var bytes = o as byte[];
            res = "0x" + ByteHelpers.ByteArrayToString(bytes);
            return res;
        }
    }

    internal class BoolToBitConverter : IFieldValueConverter
    {
        public string ConvertToSqlString(object o)
        {
            var res = "NULL";
            if (o == null)
                return res;
            if (!(o is bool))
                return res;
            res = (bool)o == true ? "1" : "0";
            return res;
        }
    }

    internal class NumberToStrConverter : IFieldValueConverter
    {
        public string ConvertToSqlString(object o)
        {
            if (o == null)
                return "NULL";
            var t = o.GetType();
            var allowedTypes = new List<Type>()
            {
                typeof(long),
                typeof(ulong),
                typeof(int),
                typeof(uint),
                typeof(short),
                typeof(ushort),
                typeof(byte),
                typeof(double),
                typeof(float),
                typeof(decimal),
            };
            if (!allowedTypes.Contains(t))
                return $"'{o}'";
            return $"{o}";
        }
    }

    internal class DateTimeToStrConv : IFieldValueConverter
    {
        public string ConvertToSqlString(object o)
        {
            var res = "NULL";
            if (o == null)
                return res;
            return $"'{o}'";
        }
    }

    internal class GuidToStrConverter : IFieldValueConverter
    {
        public string ConvertToSqlString(object o)
        {
            var res = "NULL";
            if (o == null)
                return res;
            if (!(o is Guid))
                return res;
            res = ((Guid)o).ToString();
            return $"'{res}'";
        }
    }


    /// <summary>
    /// Converts csharp string to SQL string (replaces single quotes with double single quotes)
    /// </summary>
    internal class StringToSQLStrConverter : IFieldValueConverter
    {
        public string ConvertToSqlString(object o)
        {
            var res = "NULL";
            if (o == null)
                return res;
            if (!(o is string))
                return $"'{res}'";

            var sqlString = o.ToString();
            StringBuilder sb = new StringBuilder();

            foreach (char c in sqlString)
            {
                if (c == '\'')
                    sb.Append('\'');
                sb.Append(c);
            }
            var subRes = sb.ToString();
            res = $"'{subRes}'";
            return res;
        }
    }




}
