using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.DataModel
{
    public class StaticOptions
    {
        public static class SiteCalling
        {
            public static string[] RecordType => new string[] { "Calling", "Follow-Up", "Skip", "Drop", "Partial-Drop" };
            public static string[] SurveyType1 => new string[] { "Wildlife Site", "THP", "Habitat", "Permanent Call Station" };
            public static string[] SurveyType2 => new string[] { "PS", "PF", "ACS", "SS", "OS", "HS", "SC", "RV", "BV", "RS", "SL" };
        }
    }
}
