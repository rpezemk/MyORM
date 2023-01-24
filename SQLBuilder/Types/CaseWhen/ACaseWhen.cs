using System;
using System.Collections.Generic;

namespace SQLBuilder.Types.CaseWhen
{
    public abstract class ACaseWhen
    {
        public List<SingleWhen> SingleWhens = new List<SingleWhen>();
        public object ElseValue;
        public string Alias = "";
        public Type Type { get; set; }
        public object BaseValue { get; set; }
        public ACaseWhen(ACaseWhen baseCaseWhen) 
        { 
            SingleWhens =  baseCaseWhen.SingleWhens;
            ElseValue = baseCaseWhen.ElseValue;
            Alias = baseCaseWhen.Alias;
        }
        public ACaseWhen() { }
        public abstract object CloneWithDefaultVal();
    }


}
