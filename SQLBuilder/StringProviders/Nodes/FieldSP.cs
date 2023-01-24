using SQLBuilder.StringProviders.Experimental.SResult;
using SQLBuilder.StringProviders.ProviderChain;

namespace SQLBuilder.StringProviders.Nodes
{
    /// <summary>
    /// Chain link for AField
    /// </summary>
    internal class FieldSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (typeof(AField).IsAssignableFrom(t))
            {
                var aField = condition as AField;
                var alias = string.IsNullOrEmpty(aField.TableInstance.Alias) ? "" : $"{aField.TableInstance.Alias}.";
                var fieldName = aField.AFieldName;
                return new SPResultOK() { ResultValue = $" {alias}{fieldName} " };
            }
            else
                return base.Manage(condition);
        }
    }


    internal class FieldNonAliasSP : AStringProvider
    {
        public override ASPResult Manage(object condition)
        {
            var t = condition.GetType();
            if (typeof(AField).IsAssignableFrom(t))
            {
                var aField = condition as AField;
               
                var fieldName = $"{aField.TableInstance.Alias}.{aField.AFieldName}";
                return new SPResultOK() { ResultValue = $" {fieldName} " };
            }
            else
                return base.Manage(condition);
        }
    }


}
