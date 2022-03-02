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
    public class SiteCallingRepositoryTrack : ISiteCallingTrack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid"), ForeignKey("SiteCallingRepository")]
        public Guid Guid { get; set; }

        [Required, Column("site_calling_repository_id")]
        public Guid SiteCallingRepositoryID { get; set; }
        public SiteCallingRepository SiteCallingRepository { get; set; }

        [Column("geometry", TypeName = "geometry(LineString,26710)")]
        public LineString Geometry { get; set; }


    }
}
