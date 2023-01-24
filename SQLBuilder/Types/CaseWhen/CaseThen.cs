using System;
using System.Collections.Generic;

namespace SQLBuilder.Types.CaseWhen
{
    public class CaseThen<T> : ACaseWhen
    {
        public CaseThen(ACaseWhen baseCaseWhen) : base(baseCaseWhen) { }
        public CaseWhen<T> When(ACondition condition)
        {
            SingleWhens.Add(new SingleWhen() { Condition = condition });
            return new CaseWhen<T>(this);
        }

        public CaseExpression<T> End(string alias = "") 
        {
            Alias = alias;
            return new CaseExpression<T>(this); 
        }

        public CaseElse<T> Else(object val) 
        {
            var caseElse = new CaseElse<T>(this);
            caseElse.ElseValue = val;
            return caseElse;
        }

        public override object CloneWithDefaultVal()
        {
            CaseThen<T> newcaseExpressionT = new CaseThen<T>(this);
            newcaseExpressionT.SingleWhens = new List<SingleWhen>();
            newcaseExpressionT.Type = typeof(T);
            newcaseExpressionT.BaseValue = default(T);
            return newcaseExpressionT;
        }
    }




}
