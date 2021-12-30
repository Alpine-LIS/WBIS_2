using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class OtherWildlife : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
       
        [Required, Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;

        [Required, Column("wildlife_species_id")]
        public Guid WildlifeSpeciesId { get; set; }
        public WildlifeSpecies WildlifeSpecies { get; set; }
        
        [Required, Column("site_calling_id"), ]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }
        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Other Wildlife"; } }
    }
}
