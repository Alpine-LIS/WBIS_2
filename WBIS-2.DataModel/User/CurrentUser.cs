using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WBIS_2.DataModel
{
    public static class CurrentUser
    {
        static ApplicationUser _User;
        public static ApplicationUser User
        {
            get
            {
                return _User;
            }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    if (_User != null)
                    {
                        UserName = _User.UserName;
                        ReadOnly = _User.ApplicationGroup.ReadOnly;
                        AdminPrivileges = _User.ApplicationGroup.AdminPrivileges;
                        //Districts = _User.UserDistricts;

                        CurrentUserChanged?.Invoke(new object(), new EventArgs());
                        GetVisableLayers();
                    }
                }
            }
        }

        public static string UserName { get; set; } = "Unknown";
        public static bool ReadOnly { get; set; }
        public static bool AdminPrivileges { get; set; }
        public static ICollection<District> Districts { get; set; }

        public static EventHandler CurrentUserChanged;

        public static List<Guid> ActiveBotanicalSurveyAreaGuids { get; set; } = new List<Guid>();
        public static List<Guid> ActiveHex160Guids { get; set; } = new List<Guid>();

        public static bool CurrentDatabase { get; set; } = true;
        public static bool ApplicationStarted { get; set; } = false;
        public static string UserGeoFolder { get; set; } = "MapData";

        public static List<string> CurrentInfoTypes { get; set; } = new List<string>();
        public static void AddRemoveInfoType(string infoType, bool add)
        {
            if (add) CurrentInfoTypes.Add(infoType);
            else CurrentInfoTypes.Remove(infoType);
            GetVisableLayers();
        }
        public static List<string> AllLayers { get; set; } = new List<string>();
        public static List<string> VisibleLayers { get; set; } = new List<string>();
        private static void GetVisableLayers()
        {
            WBIS2Model database = new WBIS2Model();

            VisibleLayers = database.UserMapLayers
                .Include(_ => _.ApplicationUser)
                .Where(_ => _.ApplicationUser.Guid == User.Guid && CurrentUser.CurrentInfoTypes.Contains(_.InformationType))
                .Select(_ => MapDataPasser.CleanLayerStr(_.VisibleLayer))
                .Distinct().ToList();

            if (VisibleLayers.Count() == 0)
                VisibleLayers = database.UserMapLayers
                    .Include(_ => _.ApplicationUser)
                    .Where(_ => _.ApplicationUser.Guid == User.Guid && _.InformationType == "Default View")
                    .Select(_ => MapDataPasser.CleanLayerStr(_.VisibleLayer))
                    .Distinct().ToList();
            if (VisibleLayers.Count() == 0)
                VisibleLayers = database.UserMapLayers
                    .Include(_ => _.ApplicationUser)
                    .Where(_ => _.ApplicationUser == null && _.InformationType == "Default View")
                    .Select(_ => MapDataPasser.CleanLayerStr(_.VisibleLayer))
                    .Distinct().ToList();
            
            MapDataPasser.InformationTypesChanged();
        }
    }
}
