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
    public class ProtectionZone : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }



        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }




        [Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }

        [Column("pz_id")]
        public string PZ_ID { get; set; }

        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<OwlBanding> OwlBandings { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }
        [InverseProperty("CurrentProtectionZone")]
        public ICollection<Hex160> CurrentHex160s { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager<IInformationType> Manager => (IInfoTypeManager<IInformationType>)new ProtectionZoneManager();
    }

    public class ProtectionZoneManager : IInfoTypeManager<ProtectionZone>
    {
        public string DisplayName { get { return "Proection Zone"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        [NotMapped]
        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }


        public IQueryable<ProtectionZone> GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<ProtectionZone>();
            var a = (Expression<Func<ProtectionZone, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160s).Where(a);

            return returnVal.Where(a);
        }
        public Expression<Func<ProtectionZone, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<ProtectionZone, bool>> a;
            if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            a = _ => Query.Contains(_);
            return a;
        }
    }
}
