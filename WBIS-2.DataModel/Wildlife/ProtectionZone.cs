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
        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;




        [Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }

        [Column("pz_id")]
        public string PZ_ID { get; set; }

        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<SiteCallingRepository> SiteCallingRepositories { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }
        [InverseProperty("CurrentProtectionZone")]
        public ICollection<Hex160> CurrentHex160s { get; set; }

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Proection Zone"; } }

        public IInformationType[] AvailibleChildren => throw new NotImplementedException();

        public List<KeyValuePair<string, string>> DisplayFields => throw new NotImplementedException();
    }
}
