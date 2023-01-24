using SQLBuilder.StringProviders.ForQuery;
using System.Collections.Generic;
using System.Linq;

namespace SQLBuilder
{
    public abstract class ACondition
    {
        public abstract string Keyword { get; }
        public abstract override string ToString();
    }

    public class Exists : ACondition
    {
        public SQLBuilder.Querries.SelectQueryNS.SelectQuery Query;
        public Exists(Querries.SelectQueryNS.SelectQuery query)
        {
            Query = query;
        }

        public override string Keyword => throw new System.NotImplementedException();

        public override string ToString()
        {
            return $" EXISTS ( {Query.GetString(new GeneralQueryStringProvider())} )";
        }
    }

    public class DummyCondition : ACondition
    {
        public override string Keyword => " DUMMY ";
        public override string ToString()
        {
            return Keyword;
        }
    }

    public abstract class TwoValueCondition : ACondition
    {
        public ACondition Condition1;
        public ACondition Condition2;
        public override string ToString()
        {

            var c1 = (Condition1 as TwoValueCondition) != null ? (Condition1 as TwoValueCondition) : Condition1;
            var c2 = (Condition2 as TwoValueCondition) != null ? (Condition2 as TwoValueCondition) : Condition2;

            return $"({c1} {Keyword} {c2})";
        }
    }

    public class And : TwoValueCondition
    {
        public And(ACondition condition1, ACondition condition2)
        {
            Condition1 = condition1;
            Condition2 = condition2;
        }


        public override string Keyword => " AND ";
    }
    public class Or : TwoValueCondition
    {
        public Or(ACondition condition1, ACondition condition2)
        {
            Condition1 = condition1;
            Condition2 = condition2;
        }
        public override string Keyword => " OR ";

    }



    public abstract class AIn : ACondition
    {
        public object BaseVal;
        public List<object> BaseValues;
    }


    public class In<T,T2> : AIn
    {
        public In(T val1, IEnumerable<T2> val2)
        {
            Val = val1;
            List = val2.ToList();
        }
        public T Val 
        {
            get 
            {
                if (BaseVal == null)
                    return default(T);
                try
                {
                    T res = (T)BaseVal;
                    return res;
                }
                catch
                {
                    return default(T);
                }
            }
            set
            {
                BaseVal = value;
            }
        }
        public List<T2> List 
        {
            get 
            {
                try
                {
                    var listT2 = BaseValues.Select(v => (T2)v).OfType<T2>().ToList() as List<T2>;
                    return listT2;
                }
                catch
                {
                    return new List<T2>();
                }
            }
            set 
            {
                BaseValues = value.OfType<object>().ToList();
            } 
        }
        public override string Keyword => " IN ";
        public override string ToString()
        {
            if (Val == null || List == null)
                return " 1 = 1 "; // XD

            var resultListStr = string.Join(", ", List);
            return $" {Val} IN ({resultListStr})";
        }
    }

    public class Not : ACondition
    {
        public override string Keyword => " NOT ";
        public Not(ACondition condition)
        {
            Condition = condition;
        }
        ACondition Condition;

        public override string ToString()
        {
            return $" {Keyword} {Condition})";
        }

    }

}
