using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.Tools
{
    public class DbHelp
    {
        public static string GetDbString(Type type)
        {
            string initial = type.Name;
            string returnVal = initial.First().ToString();

            for (int i = 1; i < initial.Length - 1; i++)
            {
                if (!Char.IsLetterOrDigit(initial[i]))
                    returnVal += "_";
                else if (Char.IsUpper(initial[i]) && Char.IsLower(initial[i + 1]))
                    returnVal += "_" + initial[i];
                else returnVal += initial[i];
            }
            return (returnVal + initial.Last()).ToLower();
        }
        public static string GetDbString(string type)
        {
            string initial = type;
            string returnVal = initial.First().ToString();

            for (int i = 1; i < initial.Length - 1; i++)
            {
                if (!Char.IsLetterOrDigit(initial[i]))
                    returnVal += "_";
                else if (Char.IsUpper(initial[i]) && Char.IsLower(initial[i + 1]))
                    returnVal += "_" + initial[i];
                else returnVal += initial[i];
            }
            return (returnVal + initial.Last()).ToLower();
        }

        public static string ThpQueryName(string thpName)
        {
            string initial = thpName;
            if (initial == null) initial = "";

            return initial.ToUpper().Trim();
        }
        public static THP_Area ThpExistance(WBIS2Model model, string thpName)
        {
            thpName = ThpQueryName(thpName);
            return model.THP_Areas.ToArray().FirstOrDefault(_=>ThpQueryName(_.THPName) == thpName);
        }
    }
}
