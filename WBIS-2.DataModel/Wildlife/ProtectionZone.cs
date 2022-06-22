using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = 18), TypeGrouper(GroupName = "Wildlife"), GeometryEdits(Locked = false)]
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
        public Guid? UserId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }




        [Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }

        [Column("pz_id"), ListInfo(DisplayField = true), Import(Required = true)]
        public string PZ_ID { get; set; }

        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<OwlBanding> OwlBandings { get; set; }
        [ListInfo(AutoInclude = true)]
        public ICollection<Hex160> Hex160s { get; set; }
        [InverseProperty("CurrentProtectionZone"), ListInfo(AutoInclude = true)]
        public ICollection<Hex160> CurrentHex160s { get; set; }


        //[Column("district_id")]
        //public Guid? DistrictId { get; set; }
        //public District District { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<ProtectionZone>();
    }
}
