using SQLBuilder.Types.CaseWhen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.ValueProviders.FieldProvider
{
    class FieldFactory
    {
        public static FieldFactory instance;

        /// <summary>
        /// already available Field(T)s of distinct T to clone.
        /// </summary>
        public List<AField> fieldList = new List<AField>();

        public List<ACaseWhen> caseList = new List<ACaseWhen>();
        


        /// <summary>
        /// Gets instance of Field(T) by type and value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AField GetFieldInstance(Type type, object value)
        {
            if (instance == null)
                instance = new FieldFactory();
            AField aField = instance.fieldList.Find(f => f.GetType() == type);
            if (aField == null)
            {
                aField = Activator.CreateInstance(type) as AField;
                instance.fieldList.Add(aField);
            }
            var cloned = aField.CloneWithDefaultVal() as AField;
            cloned.BVal = value;
            return cloned;
        }


        /// <summary>
        /// Gets instance of CaseExpression(T) by type and value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ACaseWhen GetCaseExpressionInstance(Type type, object value)
        {
            if (instance == null)
                instance = new FieldFactory();
            ACaseWhen baseCase = instance.caseList.Find(f => f.GetType() == type);
            if (baseCase == null)
            {
                baseCase = Activator.CreateInstance(type) as ACaseWhen;
                instance.caseList.Add(baseCase);
            }
            var cloned = baseCase.CloneWithDefaultVal() as ACaseWhen;
            cloned.BaseValue = value;
            return cloned;
        }
    }
}
