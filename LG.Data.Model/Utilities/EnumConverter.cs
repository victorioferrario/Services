using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Utilities
{
   internal static class EnumConverter
    {
       internal static T GetEnumType<T>(string value)
       {
           var returnValue = default(T);
           var t = System.Enum.GetNames(typeof(T)).ToList();
           for (var i = 0; i < t.Count() - 1; i++)
           {
               if (t[i] == value)
               {
                   return (T)System.Enum.Parse(typeof(T), value);
               }
           }
           return returnValue;
       }
    }
}