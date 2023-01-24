using System.Collections.Generic;
using System.Linq;

namespace SQLBuilder.Types.CaseWhen
{
    public class CaseWhen : ACaseWhen
    {
        public CaseWhen(ACaseWhen baseCaseWhen) : base(baseCaseWhen) { }
        public CaseWhen(ACondition condition)
        {
            SingleWhens.Add(new SingleWhen() { Condition = condition });
        }

        public override object CloneWithDefaultVal()
        {
            throw new System.NotImplementedException();
        }

        public CaseThen<T> Then<T>(T value)
        {
            if (SingleWhens.Count > 0)
                SingleWhens.Last().Value = value;
            return new CaseThen<T>(this);
        }
    }

    public class CaseWhen<T> : ACaseWhen
    {
        public CaseWhen(ACaseWhen baseCaseWhen) : base(baseCaseWhen) { }
        public CaseWhen(ACondition condition)
        {
            SingleWhens.Add(new SingleWhen() { Condition = condition });
        }

        public override object CloneWithDefaultVal()
        {
            CaseWhen<T> newcaseExpressionT = new CaseWhen<T>(this);
            newcaseExpressionT.SingleWhens = new List<SingleWhen>();
            newcaseExpressionT.Type = typeof(T);
            newcaseExpressionT.BaseValue = default(T);
            return newcaseExpressionT;
        }

        public CaseThen<T> Then(T value)
        {
            if (SingleWhens.Count > 0)
                SingleWhens.Last().Value = value;
            return new CaseThen<T>(this);
        }
    }





    public class CaseElse<T> : ACaseWhen
    {
        public CaseElse(ACaseWhen baseCaseWhen) : base(baseCaseWhen) { }
        public CaseElse(T elseValue)
        {
            ElseValue = elseValue;
        }

        public override object CloneWithDefaultVal()
        {
            CaseElse<T> newcaseExpressionT = new CaseElse<T>(this);
            newcaseExpressionT.SingleWhens = new List<SingleWhen>();
            newcaseExpressionT.Type = typeof(T);
            newcaseExpressionT.BaseValue = default(T);
            return newcaseExpressionT;
        }

        public CaseExpression<T> End(string alias = "")
        {
            Alias = alias;
            return new CaseExpression<T>(this);
        }
    }

}
