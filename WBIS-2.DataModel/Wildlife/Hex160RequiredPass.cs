using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("hex160_required_passes")]
    [DisplayOrder(Index = 17), TypeGrouper(GroupName = "Wildlife")]
    public class Hex160RequiredPass : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
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
        [Display(Order =-1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }




        [Column("user_id")]
        public Guid? UserId { get; set; }
      [ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
      [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }




        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        [ListInfo(AutoInclude = true)]
        public Hex160 Hex160 { get; set; }
        //public ICollection<District> Districts { get; set; }
        [Required, Column("bird_species_id")]
        public Guid BirdSpeciesId { get; set; }
        [ListInfo(AutoInclude = true)]
        public BirdSpecies BirdSpecies { get; set; }


        //For thematics and such
        [Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<Hex160RequiredPass>();
    }
}
