using SQLBuilder.Helpers;
using SQLBuilder.Querries.DeleteQueryNS;
using SQLBuilder.Querries.InsertQueryNS;
using SQLBuilder.Querries.SelectQueryNS;
using SQLBuilder.Querries.UpdateQueryNS;
using SQLBuilder.StringProviders.Chains;
using SQLBuilder.StringProviders.Nodes;
using SQLBuilder.StringProviders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLBuilder.StringProviders.ForQuery
{


    public class GeneralQueryStringProvider : IQueryStringProvider
    {
        public string GetString(SelectQueryModel query)
        {
            var nolock = query.WithNolock ? " with(nolock) " : "";
            var names = TupleHelpers.GetTupleFieldNames(query.QSelect);
            var whereChain = StringConvertersChain.GetAliasedProviderChainInstance();
            var whereRes = whereChain.RootProvider.Manage(query.QWhere);

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.AppendLine(query.QTop > 0 ? $" TOP {query.QTop} " : "");
            sb.AppendLine(string.Join(", ", names));
            sb.AppendLine($"FROM {query.QFrom.Schema}.{query.QFrom.ATableName} as {query.QFrom.Alias} {nolock} \n");
            foreach (var j in query.QJoins)
            {
                sb.AppendLine($"{j.AdditionalWord} JOIN {j.Table.Schema}.{j.Table.ATableName} as {j.Table.Alias} {nolock}");
                sb.AppendLine($" on {j.Condition} ");
            }
            sb.AppendLine(whereRes.ResultValue);
            return sb.ToString();
        }

        public string GetString(InsertQueryModel query)
        {
            List<(string column, string value)> colValuePairs = new List<(string column, string value)>();
            foreach (AField field in query.QColumns)
            {
                var val = "NULL"; 
                if (field.BVal != null)
                {
                    var tempVal = field.BVal;
                    var setting = TypeSettings.CSharpTypeSettings.Find(ts => ts.Type == tempVal.GetType());
                    if (setting != null)
                        val = setting.Converter.ConvertToSqlString(tempVal);
                    else
                        val = $"'{tempVal}'";
                }
                colValuePairs.Add((field.AFieldName, val));
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"INSERT INTO {query.QTable.Schema}.{query.QTable.ATableName} \n");
            sb.AppendLine($"({string.Join(", ", colValuePairs.Select(af => af.column))}) \n");
            sb.AppendLine($" VALUES({string.Join(", ", colValuePairs.Select(af => af.value))})\n\n");
            return sb.ToString();
        }

        public string GetString(UpdateQueryModel query)
        {
            List<(string column, string value)> colValuePairs = new List<(string column, string value)>();

            foreach (Set set in query.QSetList)
            {
                var col = set.Field.AFieldName;
                var val = "NULL";
                if (set.Value != null)
                {
                    var setting = TypeSettings.CSharpTypeSettings.Find(ts => ts.Type == set.Value.GetType());
                    if (setting != null)
                        val = setting.Converter.ConvertToSqlString(set.Value);
                    else
                        val = $"'{set.Value}'";
                }
                
                colValuePairs.Add((col, val));
            }

            var tableName = $"{query.QTable.Schema}.{query.QTable.ATableName}";
            var sets = string.Join(", \n", colValuePairs.Select(p => $" SET {p.column} = {p.value} "));

            var chain = StringConvertersChain.GetNonAliasedProviderChainInstance();
            var chainRes = chain.RootProvider.Manage(query.QWhere);
            var where = query.QWhere != null ? $"\n WHERE {chainRes.ResultValue}" : "";

            var sb = new StringBuilder();
            sb.AppendLine($"Update {tableName}");
            sb.AppendLine(sets);
            sb.AppendLine(where);
            sb.AppendLine("\n\n");
            var res = sb.ToString();
            return res;
        }

        public string GetString(DeleteQueryModel query)
        {
            var chain = StringConvertersChain.GetNonAliasedProviderChainInstance();
            var res = chain.RootProvider.Manage(query.QWhere);
            if (res.IsError)
                throw new Exception(res.ResultValue);
            var where = res.ResultValue;
            if (string.IsNullOrEmpty(where.Trim()))
                throw new Exception(" do not use DELETE without WHERE!!! ");
            var resQueryStr = $"DELETE FROM {query.QTable.Schema}.{query.QTable.ATableName} \n" +
                $" {where} ";

            return resQueryStr;
        }
    }




}
