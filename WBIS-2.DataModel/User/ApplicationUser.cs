using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class ApplicationUser
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Required,Column("user_name")]
        public string UserName { get; set; }
       
        [Column("email_default")]
        public string EmailDefault { get; set; }
        [Column("hint")]
        public string Hint { get; set; }
        public DateTime? deleted_ { get; set; }
        public DateTime? created_ { get; set; }
        public DateTime? modified_ { get; set; }
        [Display(Order = -1), Column("password_sha")]
        public string PasswordSHA { get; set; }
        [Column("password_time_stamp")]
        public DateTime? PasswordTimestamp { get; set; }
        [Column("user_id")]
        public string UserID { get; set; }

        [Required, Column("application_group_id")]
       public Guid ApplicationGroupId { get; set; }
        public ApplicationGroup ApplicationGroup { get; set; }

        public ICollection<OtherWildlife> OtherWildlifeRecords { get; set; }
        public ICollection<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
