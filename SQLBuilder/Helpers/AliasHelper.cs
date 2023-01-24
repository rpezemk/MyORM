using SQLBuilder.Querries.SelectQueryNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Helpers
{
    internal static class AliasHelper
    {
        internal static void MakeCorrectAliases(List<ATable> aTables)
        {
            var emptyAliases = aTables.Where(t => string.IsNullOrEmpty(t.Alias));
            var filledAliases = aTables.Where(t => !string.IsNullOrEmpty(t.Alias));

            foreach(var emptyAliasTable in emptyAliases)
            {
                emptyAliasTable.Alias = MakeShortName(emptyAliasTable.ATableName);
            }

            var dist = emptyAliases.Union(filledAliases).Select(t => t.Alias).Distinct();

            var toChange = aTables.Where(t1 => aTables.Where(t2 => t2.Alias == t1.Alias).Count() > 1).ToList();
            var c = 0;
            foreach(var toChangeTable in toChange)
            {
                toChangeTable.Alias += $"_{c}";
            }

        }


        internal static string MakeShortName(string inputStr)
        {
            if (inputStr.Length == 0)
                return "t01";
            if (inputStr.Length == 1)
                return inputStr;
            if(inputStr.ToUpper() == inputStr || inputStr.ToLower() == inputStr)
                return new string(inputStr.Take(3).ToArray());

            var len = inputStr.Length;
            var indexed = Enumerable.Range(0, len).Select(i => (@char: inputStr[i], index: i, isCapital: inputStr[i].ToString() == inputStr[i].ToString().ToUpper())).ToList();

            char prev = indexed[0].@char.FlipCap();
            var capChanged = true;
            List<char> resList = new List<char>();


            foreach (var c in indexed)
            {
                capChanged = prev.IsCapital() != c.isCapital;
                if (capChanged)
                {
                    resList.Add(c.@char);
                }
                prev = c.@char;
            }


            return new string(resList.ToArray());

        }

    }
}
