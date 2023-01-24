using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace SQLBuilder
{
    public abstract class ATable
    {
        public ATable()
        {
            object instance = this;
            if (instance == null) return;
            Type t = instance.GetType();
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, object> fields = new Dictionary<string, object>();
            foreach (PropertyInfo prp in props.Where(p => p.PropertyType.IsGenericType))
            {
                try
                {

                    object value = prp.GetValue(instance, new object[] { });
                    if (value == null )
                    {
                        if (typeof(Field<>) == prp.PropertyType.GetGenericTypeDefinition())
                        {
                            var genericArg = prp.PropertyType.GetGenericArguments()[0];
                            Type constructed = typeof(Field<>).MakeGenericType(genericArg);
                            object o2 = Activator.CreateInstance(constructed);
                            prp.SetValue(instance, o2);
                        }
                        else if (typeof(IdField<>) == prp.PropertyType.GetGenericTypeDefinition())
                        {
                            var genericArg = prp.PropertyType.GetGenericArguments()[0];
                            Type constructed = typeof(IdField<>).MakeGenericType(genericArg);
                            object o2 = Activator.CreateInstance(constructed);
                            prp.SetValue(instance, o2);
                        }
                        else
                            continue;
                    }
                    value = prp.GetValue(instance, new object[] { });
                    var propType = value.GetType();
                    var propName = value.GetType().Name;
                    var aField = value as AField;
                    if (aField != null)
                    {
                        aField.TableInstance = instance as ATable;
                        aField.AFieldName = prp.Name;
                    }
                    
                }
                catch (Exception ex)
                {

                }
            }
        }

        public string ATableName { get => this.GetType().Name; }
        public abstract string Schema { get; }
        public virtual string Alias { get; set; }
    }
}
