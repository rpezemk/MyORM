using SQLBuilder.Querries;


namespace SQLBuilder
{
    public abstract class Unary : ACondition 
    {
        public object Val;
    }

    public class IS_NULL<T1> : Unary where T1: new()
    {
        T1 Val = new T1() ;
        public IS_NULL(T1 t1)
        {
            Val = t1;
        }
        public override string Keyword
        {
            get => " IS NULL ";
        }

        public override string ToString()
        {
            return $" {Val} {Keyword} ";
        }
    }

    public abstract class AComparable : ACondition
    {
        public object Val1;
        public object Val2;
        public override string ToString()
        {
            var val1 = (Val1 as AField) != null ? (Val1 as AField).ToString() : $"'{Val1}'";
            var val2 = (Val2 as AField) != null ? (Val2 as AField).ToString() : $"'{Val2}'";
            return $"{val1} {Keyword} {val2}";
        }
    }

    public class Equals<T1, T2> : AComparable
    {
        public Equals(){}

        public Equals(T1 field, T2 field2)
        {
            Val1 = field;
            Val2 = field2;
        }

        public override string Keyword
        {
            get => " = ";
        }
    }

    public class LessThan<T1, T2> : AComparable
    {

        public LessThan()
        {

        }

        public LessThan(T1 field, T2 field2)
        {
            Val1 = field;
            Val2 = field2;
        }


        public override string Keyword
        {
            get => " < ";
        }
    }


    public class LessOrEquals<T1, T2> : AComparable
    {
        public LessOrEquals() {}

        public LessOrEquals(T1 field, T2 field2)
        {
            Val1 = field;
            Val2 = field2;
        }
        public override string Keyword
        {
            get => " <= ";
        }
    }

    public class GreaterThan<T1, T2> : AComparable
    {
        public GreaterThan() { }

        public GreaterThan(T1 field, T2 field2)
        {
            Val1 = field;
            Val2 = field2;
        }

        public override string Keyword
        {
            get => " > ";
        }
    }


    public class GreaterOrEquals<T1, T2> : AComparable
    {
        public GreaterOrEquals() { }

        public GreaterOrEquals(T1 field, T2 field2)
        {
            Val1 = field;
            Val2 = field2;
        }


        public override string Keyword
        {
            get => " >= ";
        }
    }

    public class Like<T1, T2> : AComparable
    {
        public override string Keyword => " LIKE ";
        public Like(T1 val1, T2 val2)
        {
            Val1 = val1;
            Val2 = val2;
        }
    }


    public class NotEq<T1, T2> : AComparable
    {
        public NotEq(T1 field, T2 field2)
        {
            Val1 = field;
            Val2 = field2;
        }
        public override string Keyword
        {
            get => " <> ";
        }
    }



}
