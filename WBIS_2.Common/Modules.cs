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

        public static string ModuleCnddbOccurrences { get { return "CnddbOccurrences"; } }
        public static string ModuleCnddbQuadElements { get { return "CnddbQuadElements"; } }
        public static string ModuleCdfwSpottedOwls { get { return "CdfwSpottedOwls"; } }



        public static string ModulePlantSpeciesList { get { return "PlantSpeciesList"; } }
        public static string ModuleAmphibianSpeciesList { get { return "AmphibianSpeciesList"; } }
        public static string ModuleBirdSpeciesList { get { return "BirdSpeciesList"; } }
        public static string ModuleWildlifeSpeciesList { get { return "WildlifeSpeciesList"; } }



        public static string ModuleBotanicalScoping { get { return "BotnaicalScoping"; } }
        public static string ModuleBotanicalSurveyArea { get { return "BotnaicalSurveyArea"; } }
        public static string ModuleBotanicalSurvey { get { return "BotnaicalSurvey"; } }
        public static string ModuleBotanicalSurveyElement { get { return "BotanicalSurveyElement"; } }


        public static string ModulePermanentCallStation { get { return "PermanentCallStation"; } }
        public static string ModuleProtectionZone { get { return "ProtectionZone"; } }
        public static string ModuleHex160RequiredPasses { get { return "Hex160RequiredPasses"; } }
        public static string ModuleSiteCallings { get { return "SiteCallings"; } }
        public static string ModuleSiteCallingDetections { get { return "SiteCallingDetections"; } }
        public static string ModuleOwlBandings { get { return "OwlBandings"; } }

        public static string ModuleAmphibianSurveys { get { return "AmphibianSurveys"; } }
        public static string ModuleAmphibianElements { get { return "AmphibianElements"; } }


        public static string ModuleAdditionalSurveyTemplates { get { return "AdditionalSurveyTemplates"; } }
        public static string ModuleAdditionalSurveys { get { return "AdditionalSurveys"; } }


        public static string ModuleDistrictReport => "DistrictReport";
        public static string ModuleBotanicalReports => "BotanicalReports";
        public static string ModuleReportBuilder => "ReportBuilder";
    }
}
