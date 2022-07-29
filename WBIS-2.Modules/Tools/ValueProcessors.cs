using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.Modules.Tools
{
    public class ValueProcessors
    {
        public static object GetParseValue(object val, Type destination)
        {
            if (val.GetType() == destination) return val;
            if (destination == typeof(string)) return GetTestValue(val);
            else if (destination == typeof(bool)) return TryDoubleParse(val);
            else if (destination == typeof(int)) return TryIntParse(val);
            else if (destination == typeof(double)) return TryDoubleParse(val);
            else if (destination == typeof(DateTime)) return TryDoubleParse(val);
            else return val;
        }


        public static int TryIntParse(object val)
        {
            double numericVal = TryDoubleParse(val);
            return Convert.ToInt32(Math.Floor(Convert.ToDouble(numericVal)));
        }
        public static double TryDoubleParse(object val)
        {
            string testVal = GetTestValue(val);
            double returnVal = 0d;
            double.TryParse(testVal, out returnVal);
            return returnVal;
        }
        public static bool TryBoolParse(object val)
        {
            string testVal = GetTestValue(val);
            bool returnVal = TryBoolParseString(testVal);
            return returnVal;
        }
        public static bool TryBoolParseString(string val)
        {
            bool returnVal = false;
            if (bool.TryParse(val, out returnVal))
                return returnVal;
            else
                return val.ToUpper().Trim() == "TRUE"
                    || val.ToUpper().Trim() == "T"
                    || val.ToUpper().Trim() == "Y"
                    || val.ToUpper().Trim() == "YES";
        }
        public static DateTime TryDateTimeParse(object val)
        {
            string testVal = GetTestValue(val);
            DateTime returnVal = DateTime.MinValue;
            DateTime.TryParse(testVal, out returnVal);
            return returnVal;
        }
        public static string GetTestValue(object val)
        {
            if (val is DBNull) return "";
            if (val == null) return "";
            return val.ToString();
        }

        public static string BuildWshdtring(string waterNum)
        {
            var vals = waterNum.Split('.');
            while (vals[0].Length < 4)
                vals[0] = "0" + vals[0];
            if (vals.Length == 1)
                vals = vals.Append("000000").ToArray();
            while (vals[1].Length < 6)
                vals[1] = vals[1] + "0";
            return vals[0] + "." + vals[1];
        }
    }
}
