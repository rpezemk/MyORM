using System;

namespace SQLBuilder
{
    public abstract class AField 
    {
        /// <summary>
        /// Base value object
        /// </summary>
        public object BVal { get; set; }
        public string AFieldName { get; set; }
        public ATable TableInstance { get; set; }
        public bool IsNullable = false;
        public string GetSQLColumn()
        {
            return $"{TableInstance.Alias}.{AFieldName}";
        }
        public override string ToString()
        {
            return $"{TableInstance.Alias}.{AFieldName}";
        }

        public abstract object CloneWithDefaultVal();

    }
}
