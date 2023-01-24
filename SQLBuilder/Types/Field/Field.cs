using System;
using System.Runtime.CompilerServices;

namespace SQLBuilder
{

    public class Field<T> : AField
    {
        public Field(ATable tableInstance)
        {
            if(tableInstance != null)
                TableInstance = tableInstance;
            Val = default(T);
            IsNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
        }

        public Field()
        {
            Val = default(T);
            IsNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
        }

        public T Val { get => (T)BVal; set => BVal = value; }

        public override object CloneWithDefaultVal()
        {
            Field<T> fieldT = new Field<T>();
            fieldT.BVal = default(T);
            fieldT.AFieldName = AFieldName;
            fieldT.TableInstance = TableInstance;
            return fieldT;
        }
    }

    public class IdField<T> : Field<T>
    {
        public IdField(ATable tableInstance)
        {
            if (tableInstance != null)
                TableInstance = tableInstance;
        }

        public IdField() { }

        public new T Val { get => val; set => val = value; }
        protected T val;

    }



}
