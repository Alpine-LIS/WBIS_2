using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
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

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<object, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<object, bool>> a;
            if (QueryType == typeof(District))
                a = _ => ((SiteCalling)_).Hex160.Districts.Any(d => Query.Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => ((SiteCalling)_).Hex160.Quad75s.Any(d => Query.Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => ((SiteCalling)_).Hex160.Watersheds.Any(d => Query.Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Contains(((SiteCalling)_).Hex160);
            else
                a = _ => Query.Contains(((SiteCalling)_));
            return a;
        }
    }
}
