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
    public class PermanentCallStation : UserDataValidator, IUserRecords
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

        [Column("geometry"), DataType("geometry(Point,26710)")]
        public Point Geometry { get; set; }

        [Column("pcs_id")]
        public string PCS_ID { get; set; }

        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }

        public ICollection<SiteCalling> SiteCallings { get; set; }

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Permanent Call Stations"; } }

        public IInformationType[] AvailibleChildren => throw new NotImplementedException();

        public List<KeyValuePair<string, string>> DisplayFields => throw new NotImplementedException();
    }
}
