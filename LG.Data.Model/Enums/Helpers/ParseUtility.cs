using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums.Helpers
{
    public static class ParseUtility
    {
        public static bool GetEnumValue<T>(string input, out T returnValue)
        where T : struct, IComparable, IFormattable, IConvertible
        {
            bool flag;
            if (!System.Enum.IsDefined(typeof(T), input))
            {
                string[] values = System.Enum.GetNames(typeof(T));
                returnValue = (T)System.Enum.Parse(typeof(T), values[0], true);
                flag = false;
            }
            else
            {
                returnValue = (T)System.Enum.Parse(typeof(T), input, true);
                flag = true;
            }
            return flag;
        }

        public static bool TryParse<T>(string valueToParse, out T returnValue)
        {
            int intEnumValue;
            bool flag;
            returnValue = default(T);
            if ((!int.TryParse(valueToParse, out intEnumValue)
                || !System.Enum.IsDefined(typeof(T), intEnumValue)))
            {
                flag = false;
            }
            else
            {
                returnValue = (T)(object)intEnumValue;
                flag = true;
            }
            return flag;
        }

        public static bool TryParse<T>(int intEnumValue, out T returnValue)
        {
            bool flag;
            returnValue = default(T);
            if (!System.Enum.IsDefined(typeof(T), intEnumValue))
            {
                flag = false;
            }
            else
            {
                returnValue = (T)(object)intEnumValue;
                flag = true;
            }
            return flag;
        }
    }
}
