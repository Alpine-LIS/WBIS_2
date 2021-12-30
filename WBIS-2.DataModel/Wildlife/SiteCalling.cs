using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class SiteCalling : UserDataValidator, IUserRecords
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
        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }



        [Required, Column("bird_species_survey_id")]
        public Guid SurveySpeciesId { get; set; }
        public BirdSpecies SurveySpecies { get; set; }
        [Required, Column("bird_species_found_id")]
        public Guid SpeciesFoundId { get; set; }
        public BirdSpecies SpeciesFound { get; set; }



        public ICollection<OtherWildlife> OtherWildlifeRecords { get; set; }
        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "SIte Calling"; } }
    }
}
