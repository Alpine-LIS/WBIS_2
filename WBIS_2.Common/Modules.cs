using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.Common
{
    public static class Modules
    {
        public static string Main { get { return "Main"; } }
        public static string ModuleUser { get { return "ApplicationUser"; } }
        public static string ModuleAdminUser { get { return "ApplicationAdminUser"; } }


        public static string ModuleDistricts { get { return "District"; } }
        public static string ModuleWatersheds { get { return "Watershed"; } }
        public static string ModuleQuad75s { get { return "Quad75"; } }
        public static string ModuleHex160s { get { return "Hex160"; } }

        public static string ModuleCnddbOccurrence { get { return "CnddbOccurrence"; } }
        public static string ModuleCdfwSpottedOwl { get { return "CdfwSpottedOwl"; } }



        public static string ModulePlantsList { get { return "PlantsList"; } }



        public static string ModuleBotanicalScoping { get { return "BotnaicalScoping"; } }
        public static string ModuleBotanicalSurveyArea { get { return "BotnaicalSurveyArea"; } }
        public static string ModuleBotanicalSurvey { get { return "BotnaicalSurvey"; } }
        public static string ModuleBotanicalSurveyElement { get { return "BotanicalSurveyElement"; } }


        public static string ModulePermanentCallStation { get { return "PermanentCallStation"; } }
        public static string ModuleProtectionZone { get { return "ProtectionZone"; } }
    }
}
