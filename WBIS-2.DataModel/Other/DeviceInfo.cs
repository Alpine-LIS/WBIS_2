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
    public class DeviceInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("site_calling_id")]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }


        [Column("device_time")]
        public DateTime DeviceTime { get; set; }
        
        [Column("geometry"), DataType("geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("device_lat")]
        public double DeviceLat { get; set; }
        [Column("device_lon")]
        public double DeviceLon { get; set; }
        

    }
}
