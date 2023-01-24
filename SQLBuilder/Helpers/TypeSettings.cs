using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Helpers
{
    internal static class TypeSettings
    {

        public static List<TypeSetting> CSharpTypeSettings = new List<TypeSetting>()
        {
            new TypeSetting(){ Type = typeof(long) },
            new TypeSetting(){ Type = typeof(byte[]), Converter = new ByteToSQLStringConverter() },
            new TypeSetting(){ Type = typeof(bool), Converter = new BoolToBitConverter() },
            new TypeSetting(){ Type = typeof(string), Converter = new StringToSQLStrConverter() },
            new TypeSetting(){ Type = typeof(DateTime), Converter = new DateTimeToStrConv() },
            new TypeSetting(){ Type = typeof(decimal), Converter = new NumberToStrConverter() },
            new TypeSetting(){ Type = typeof(double), Converter = new NumberToStrConverter() },
            new TypeSetting(){ Type = typeof(int), Converter = new NumberToStrConverter() },
            new TypeSetting(){ Type = typeof(float), Converter = new NumberToStrConverter() },
            new TypeSetting(){ Type = typeof(Guid), Converter = new GuidToStrConverter() },
            new TypeSetting(){ Type = typeof(short), Converter = new NumberToStrConverter()},
            new TypeSetting(){ Type = typeof(byte), Converter = new NumberToStrConverter() },
            new TypeSetting(){ Type = typeof(DateTimeOffset), Converter = new DateTimeToStrConv()},
        };
        public static Type GetClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long?);
                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool?);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime?);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal?);

                case SqlDbType.Float:
                    return typeof(double?);

                case SqlDbType.Int:
                    return typeof(int?);

                case SqlDbType.Real:
                    return typeof(float?);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid?);

                case SqlDbType.SmallInt:
                    return typeof(short?);

                case SqlDbType.TinyInt:
                    return typeof(byte?);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
    }


}
