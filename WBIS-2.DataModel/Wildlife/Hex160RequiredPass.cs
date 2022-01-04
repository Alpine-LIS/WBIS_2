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
    public class Hex160RequiredPass : UserDataValidator, IUserRecords, IQueryStuff<Hex160RequiredPass>
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

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<Hex160RequiredPass, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Hex160RequiredPass, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Hex160.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Hex160.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Hex160.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            else
                a = _ => Query.Contains(_);
            return a;
        }
    }
}
