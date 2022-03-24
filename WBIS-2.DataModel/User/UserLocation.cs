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
    public class UserLocation: IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Column("site_calling_detection_id"), ForeignKey("SiteCallingDetection")]
        public Guid SiteCallingDetectionId { get; set; }
        public SiteCallingDetection SiteCallingDetection { get; set; }



        [Required, Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        /// <summary>
        /// User Lat
        /// </summary>
        [Column("lat")]
        public double Lat { get; set; }
        /// <summary>
        /// User Lon
        /// </summary>
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }
    }
}
