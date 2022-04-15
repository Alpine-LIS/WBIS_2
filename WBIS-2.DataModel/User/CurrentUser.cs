using System;
using System.Collections.Generic;
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

    }
}
