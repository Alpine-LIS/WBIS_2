using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class ApplicationUser: IInformationType
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Required,Column("user_name")]
        public string UserName { get; set; }
       
        [Column("email")]
        public string EmailDefault { get; set; }
        [Column("hint")]
        public string Hint { get; set; }

        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }

        [Display(Order = -1), Column("password_sha")]
        public string PasswordSHA { get; set; }
        [Column("password_time_stamp")]
        public DateTime? PasswordTimestamp { get; set; }
        [Column("user_id")]
        public string UserID { get; set; }

        [Required, Column("application_group_id")]
       public Guid ApplicationGroupId { get; set; }
        public ApplicationGroup ApplicationGroup { get; set; }

        [Column("modified_user_id")]
        public Guid AdminId { get; set; }
        public ApplicationUser Admin { get; set; }

        public ICollection<ApplicationUser> Contractors { get; set; }
        public ICollection<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<SiteCallingRepository> SiteCallingRepositories { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<ProtectionZone> ProtectionZones { get; set; }
        public ICollection<PermanentCallStation> PermanentCallStations { get; set; }

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Application User"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("UserName", "User")};
            }
        }
    }
}
