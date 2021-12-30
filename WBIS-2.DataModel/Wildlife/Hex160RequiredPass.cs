using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class Hex160RequiredPass : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; } 
        [Column("required_passes")]
        public int RequiredPasses { get; set; }
        [Column("current_passes")]
        public int CurrentPasses { get; set; }
        [Column("dropped")]
        public bool Dropped { get; set; } = false;
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }

        [Required, Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;
        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }
        [Required, Column("bird_species_id")]
        public Guid BirdSpeciesId { get; set; }
        public BirdSpecies BirdSpecies { get; set; }
        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Hex160 Required Passes"; } }
    }
}
