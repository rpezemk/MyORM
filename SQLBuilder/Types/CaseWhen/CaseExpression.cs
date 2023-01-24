using System.Collections.Generic;

namespace SQLBuilder.Types.CaseWhen
{
    public class CaseExpression<T> : ACaseWhen
    {
        public CaseExpression(object val)
        {

        }
        public CaseExpression(T val)
        {
            Val = val;
        }
        public CaseExpression() { }
        public CaseExpression(ACaseWhen baseCaseWhen) : base(baseCaseWhen)
        {
            if(BaseValue != null)
                Val = (T)BaseValue;
        }

        public T? Val { get => BaseValue == System.DBNull.Value ? default(T) : (T)BaseValue; set => BaseValue = value; }

        public override object CloneWithDefaultVal()
        {
            CaseExpression<T> newcaseExpressionT = new CaseExpression<T>();
            newcaseExpressionT.SingleWhens = new List<SingleWhen>();
            newcaseExpressionT.Type = typeof(T);
            newcaseExpressionT.BaseValue = default(T);
            return newcaseExpressionT;
        }
    }


}
