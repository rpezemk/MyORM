using SQLBuilder.Helpers;
using SQLBuilder.StringProviders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Querries.UpdateQueryNS
{

    public class UpdateQueryModel : IStringableQuery
    {
        public UpdateQueryModel()
        {

        }
        public UpdateQueryModel(UpdateQueryModel  updateQueryModel)
        {
            if (updateQueryModel == null)
                return;
            if(updateQueryModel.QTable != null)
                QTable = updateQueryModel.QTable;
            if(updateQueryModel.QSetList != null)
                QSetList = updateQueryModel.QSetList;
            if(updateQueryModel.QWhere != null)
                QWhere = updateQueryModel.QWhere;
        }
        public ATable QTable;
        public List<Set> QSetList = new List<Set>();
        public ACondition QWhere;

        public override string ToString()
        {


            List<(string column, string value)> colValuePairs = new List<(string column, string value)>();

            foreach (Set set in QSetList)
            {
                var col = set.Field.AFieldName;
                var val = string.Empty;
                if (set.Value != null)
                {
                    var tempVal = set.Value;
                    var t = tempVal.GetType();
                    var a = TypeSettings.CSharpTypeSettings.Find(ts => ts.Type == t);
                    if (a != null)
                        val = a.Converter.ConvertToSqlString(tempVal);
                    else
                        val = $"'{tempVal}'";
                }
                else
                {
                    val = "NULL";
                }
                colValuePairs.Add((col, val));
            }

            var tableName = $"{QTable.Schema}.{QTable.ATableName}";
            var sets = string.Join(", \n", colValuePairs.Select(p => $" SET {p.column} = {p.value} "));
            var where = QWhere != null ? $"\n WHERE {QWhere}" : "";

            var sb = new StringBuilder();
            sb.AppendLine($"Update {tableName}");
            sb.Append(sets);
            sb.AppendLine(where);
            var res = sb.ToString();
            return res;
        }

        public string GetString(IQueryStringProvider stringProvider)
        {
            return stringProvider.GetString(this);
        }
    }
}
