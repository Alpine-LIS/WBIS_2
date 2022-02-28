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
    public class UserLocation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Column("site_calling_detection_id")]
        public Guid SiteCallingDetectionId { get; set; }
        public SiteCallingDetection SiteCallingDetection { get; set; }

        [Required, Column("geometry"), DataType("geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Required, Column("user_lat")]
        public double UserLat { get; set; }
        [Required, Column("user_lon")]
        public double UserLon { get; set; }
    }
}
