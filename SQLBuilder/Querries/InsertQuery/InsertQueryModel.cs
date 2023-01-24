using SQLBuilder.Helpers;
using SQLBuilder.Querries.SelectQueryNS;
using SQLBuilder.StringProviders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Querries.InsertQueryNS
{
    public class InsertQueryModel : IStringableQuery
    {
        internal ATable QTable;
        internal List<AField> QColumns = new List<AField>();
        internal ATable QValues;
        public override string ToString()
        {
            List<(string column, string value)> colValuePairs = new List<(string column, string value)>();

            foreach (AField field in QColumns)
            {
                var col = field.AFieldName;
                var val = "";
                if (field.BVal != null)
                {
                    var tempVal = field.BVal;
                    var t = tempVal.GetType();
                    var setting = TypeSettings.CSharpTypeSettings.Find(ts => ts.Type == t);
                    if (setting != null)
                        val = setting.Converter.ConvertToSqlString(tempVal);
                    else
                        val = $"'{tempVal}'";
                }
                else
                {
                    val = "NULL";
                }
                colValuePairs.Add((col, val));
            }



            var resQueryStr = $"INSERT INTO {QTable.Schema}.{QTable.ATableName} \n" +
                $"({string.Join(", ", colValuePairs.Select(af => af.column))}) \n" +
                $" VALUES({string.Join(", ", colValuePairs.Select(af => af.value))})";

            return resQueryStr;
        }

        public string GetString(IQueryStringProvider stringProvider)
        {
            return stringProvider.GetString(this);
        }
    }
}
