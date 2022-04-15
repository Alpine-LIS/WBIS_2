using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.Modules.Tools
{
    public class ValueProcessors
    {
        public static int? TryIntParse(object val, Type destination)
        {
            string testVal = GetTestValue(val);
            int returnVal = 0;
            if (int.TryParse(testVal, out returnVal))
                return returnVal;
            else
            {
                if (Nullable.GetUnderlyingType(destination) != null)
                    return null;
                else return returnVal;
            }
        }
        public static double? TryDoubleParse(object val, Type destination)
        {
            string testVal = GetTestValue(val);
            double returnVal = 0d;
            if (double.TryParse(testVal, out returnVal))
                return returnVal;
            else
            {
                if (Nullable.GetUnderlyingType(destination) != null)
                    return null;
                else return returnVal;
            }
        }
        public static bool? TryBoolParse(object val, Type destination)
        {
            string testVal = GetTestValue(val);
            bool returnVal = false;
            if (bool.TryParse(testVal, out returnVal))
                return returnVal;
            else
            {
                if (Nullable.GetUnderlyingType(destination) != null)
                    return null;
                else return returnVal;
            }
        }
        public static DateTime? TryDateTimeParse(object val,Type destination)
        {
            string testVal = GetTestValue(val);
            DateTime returnVal = DateTime.Now;
            if (DateTime.TryParse(testVal, out returnVal))
                return returnVal;
            else
            {
                if (Nullable.GetUnderlyingType(destination) != null)
                    return null;
                else return returnVal;
            }
        }
        public static string GetTestValue(object val)
        {
            if (val is DBNull) return "";
            if (val == null) return "";
            return val.ToString();
        }
    }
}
