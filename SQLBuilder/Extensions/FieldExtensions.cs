using SQLBuilder.Types.CaseWhen;

namespace SQLBuilder.Extensions
{
    public static class FieldExtensions
    {
        public static ACondition Eq<T, T2>(this T field, T2 field2) where T : new()
        {
            return new Equals<T, T2>(field, field2) { };
        }

        public static ACondition NotEq<T, T2>(this T field, T2 field2) where T : new()
        {
            return new NotEq<T, T2>(field, field2) { };
        }

        public static ACondition LessThan<T, T2>(this T field, T2 field2) where T : new()
        {
            return new LessThan<T, T2>(field, field2) { };
        }

        public static ACondition GreaterThan<T, T2>(this T field, T2 field2) where T : new()
        {
            return new GreaterThan<T, T2>(field, field2) { };
        }

        public static ACondition LessOrEq<T, T2>(this T field, T2 field2) where T : new()
        {
            return new LessOrEquals<T, T2>(field, field2) { };
        }

        public static ACondition GreaterOrEq<T, T2>(this T field, T2 field2) where T : new()
        {
            return new GreaterOrEquals<T, T2>(field, field2) { };
        }

        public static ACondition In<T1, T2>(this T1 field, params T2[] enumerable)
        {
            return new In<T1, T2>(field, enumerable);
        }

        public static ACondition Like<T, T2>(this T field, T2 field2) where T : new()
        {
            return new Like<T, T2>(field, field2) { };
        }


    }
}
