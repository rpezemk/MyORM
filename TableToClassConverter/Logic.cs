using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableToClassConverter.Model;

namespace TableToClassConverter
{
    internal static class Logic
    {
        internal static string GetCSharpClasses(List<SchemaInfo> schemaInfos)
        {
            var schemaGroups = schemaInfos.GroupBy(i => i.TableSchema);
            var resStr = "";
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            foreach (var schemaGroup in schemaGroups)
            {
                var schemaName = schemaGroup.Key;

                var tableGroups = schemaGroup.GroupBy(i => i.TableName);
                foreach(var tableGroup in tableGroups)
                {
                    List<string> vars = new List<string>();
                    var tableName = tableGroup.Key;
                    foreach(var column in tableGroup)
                    {
                        var varLine = "";
                        var colName = TranslateToProperIdentifier(column.ColumnName, codeProvider);
                        var colDataType = FromSqlType(column.DataType);
                        if(column.IsNullable)
                            varLine = $"        public Field<{colDataType}?> {colName} {"{ get; set; }"}\n";
                        else 
                            varLine = $"        public Field<{colDataType}> {colName} {"{ get; set; }"}\n";
                        vars.Add(varLine);
                    }

                    var varLines = string.Join("", vars);
                    var classLines = $"    public class {tableName} : ATable\r\n    {"{"} \n" ;
                    classLines += $"        public override string Schema => \"{schemaName}\";\n";
                    classLines += varLines;
                    classLines += "    } \n\n\n";
                    resStr += classLines;
                }
            }

            return resStr;
        }


        internal static List<SchemaInfo> GetInformationSchema(string server, string db, string login, string pwd, string schema = "")
        {
            List<SchemaInfo> schemas = new List<SchemaInfo>();
            DataTable table = new DataTable();
            var query = $@"
                        select T.TABLE_SCHEMA, T.TABLE_NAME, C.COLUMN_NAME, C.DATA_TYPE, 
                        case WHEN ISNULL(C.IS_NULLABLE, 'YES') = 'YES' THEN 1 ELSE 0 END as [IS_NULLABLE]
                        from information_schema.tables T
                        inner join INFORMATION_SCHEMA.COLUMNS C
                            on c.TABLE_NAME = T.TABLE_NAME
                            and c.TABLE_SCHEMA = T.TABLE_SCHEMA
                        {(schema.IsNullOrEmpty() ? "": $" where T.Table_Schema = '{schema.Replace("'", "''")}")}'";

            try
            {
                table = SQL.RunQueryToDataTable(
                    new System.Data.SqlClient.SqlConnection(
                        new SqlConnectionStringBuilder() 
                        { 
                            DataSource = server, 
                            InitialCatalog = db, 
                            UserID = login, 
                            Password = pwd
                        }.ToString()), query);
                foreach (DataRow row in table.Rows)
                {
                    SchemaInfo schemaInfo = new SchemaInfo();
                    schemaInfo.TableSchema = row["TABLE_SCHEMA"].ToString();
                    schemaInfo.TableName = row["TABLE_NAME"].ToString();
                    schemaInfo.ColumnName = row["COLUMN_NAME"].ToString();
                    schemaInfo.DataType = row["DATA_TYPE"].ToString();
                    schemaInfo.IsNullable = Convert.ToInt32(row["IS_NULLABLE"]) == 1;
                    schemas.Add(schemaInfo);
                }
            }
            catch (Exception ex) { throw ex; }


            return schemas;
        }

        public static string TranslateToProperIdentifier(string columnName, CSharpCodeProvider cs)
        {
            var res = columnName.ReplaceAllWith(" -()", '_');
            if (cs == null)
                return res;

            if (cs.IsValidIdentifier(res))
                return res;
            else
                return new string(new[] { res[0] }).ToUpper() + new string(res.Skip(1).ToArray()); 
        }

        public static string FromSqlType(string sqlTypeString)
        {
            if (!Enum.TryParse(sqlTypeString, out SQLType typeCode))
            {
                throw new Exception("sql type not found");
            }
            switch (typeCode)
            {
                case SQLType.varbinary:
                case SQLType.binary:
                case SQLType.filestream:
                case SQLType.image:
                case SQLType.rowversion:
                case SQLType.timestamp://?
                    return "byte[]";
                case SQLType.tinyint:
                    return "byte";
                case SQLType.varchar:
                case SQLType.nvarchar:
                case SQLType.nchar:
                case SQLType.text:
                case SQLType.ntext:
                case SQLType.xml:
                    return "string";
                case SQLType.@char:
                    return "char";
                case SQLType.bigint:
                    return "long";
                case SQLType.bit:
                    return "bool";
                case SQLType.smalldatetime:
                case SQLType.datetime:
                case SQLType.date:
                case SQLType.datetime2:
                    return "DateTime";
                case SQLType.datetimeoffset:
                    return "DateTimeOffset";
                case SQLType.@decimal:
                case SQLType.money:
                case SQLType.numeric:
                case SQLType.smallmoney:
                    return "decimal";
                case SQLType.@float:
                    return "double";
                case SQLType.@int:
                    return "int";
                case SQLType.real:
                    return "Single";
                case SQLType.smallint:
                    return "short";
                case SQLType.uniqueidentifier:
                    return "Guid";
                case SQLType.sql_variant:
                    return "object";
                default:
                    throw new Exception("none equal type");
            }
        }

        public enum SQLType
        {
            varbinary,//(1)
            binary,//(1)
            image,
            varchar,
            @char,
            nvarchar,//(1)
            nchar,//(1)
            text,
            ntext,
            uniqueidentifier,
            rowversion,
            bit,
            tinyint,
            smallint,
            @int,
            bigint,
            smallmoney,
            money,
            numeric,
            @decimal,
            real,
            @float,
            smalldatetime,
            datetime,
            sql_variant,
            table,
            cursor,
            timestamp,
            xml,
            date,
            datetime2,
            datetimeoffset,
            filestream,

        }

    }
}
