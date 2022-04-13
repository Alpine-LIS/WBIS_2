using Microsoft.EntityFrameworkCore;
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
        [Display(Order =-1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }




        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }




        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }
        [Required, Column("bird_species_id")]
        public Guid BirdSpeciesId { get; set; }
        public BirdSpecies BirdSpecies { get; set; }


        //For thematics and such
        [Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager<IInformationType> Manager => (IInfoTypeManager<IInformationType>)new Hex160RequiredPassManager();
    }

    public class Hex160RequiredPassManager : IInfoTypeManager<Hex160RequiredPass>
    {
        public string DisplayName { get { return "Hex160 Required Passes"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public IQueryable<Hex160RequiredPass> GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<Hex160RequiredPass>();
            var a = (Expression<Func<Hex160RequiredPass, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160).Where(a);

            return returnVal.Where(a);
        }
        public Expression<Func<Hex160RequiredPass, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Hex160RequiredPass, bool>> a;
            if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
