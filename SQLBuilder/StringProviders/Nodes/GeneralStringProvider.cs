using System;
using System.Reflection.Metadata.Ecma335;
using SQLBuilder.Helpers;
using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{

    /// <summary>
    /// Chain link for unresolved types
    /// </summary>
    internal class GeneralStringProvider : AStringProvider
    {

        public override ASPResult Manage(object condition)
        {
            try
            {
                var t = condition.GetType();
                var setting = TypeSettings.CSharpTypeSettings.Find(ts => ts.Type == t);
                var res = "";
                if (setting == null)
                    return new SPResultOK() { ResultValue = $"{condition}" };
                else
                    res = setting.Converter.ConvertToSqlString(condition);
                return new SPResultOK() { ResultValue = $"{res}" };
            }
            catch (Exception ex)
            {
                return new SPErrorResult() { ResultValue = ex.Message };
            }
        }
    }

}
