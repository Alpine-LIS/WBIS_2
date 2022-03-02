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
    public class OtherWildlife : UserDataValidator, IUserRecords, IQueryStuff<OtherWildlife>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
       
        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;




        [Required, Column("wildlife_species_id")]
        public Guid WildlifeSpeciesId { get; set; }
        public WildlifeSpecies WildlifeSpecies { get; set; }
        
        [Column("site_calling_id"), ]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

        [Column("site_calling_repository_id"),]
        public Guid SiteCallingRepositoryId { get; set; }
        public SiteCallingRepository SiteCallingRepository { get; set; }

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Other Wildlife"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<OtherWildlife, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<OtherWildlife, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.SiteCalling.Hex160.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.SiteCalling.Hex160.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.SiteCalling.Hex160.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.SiteCalling.Hex160);
            else
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
