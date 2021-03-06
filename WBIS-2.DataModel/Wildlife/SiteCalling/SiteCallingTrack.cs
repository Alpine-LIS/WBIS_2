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
    [Table("site_calling_tracks")]
    [SubElement(typeof(SiteCalling)), GeometryEdits(Locked = false)]
    public class SiteCallingTrack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id"), ForeignKey("SiteCalling")]
        public Guid Id { get; set; }

        //[Required, Column("site_calling_id"), ForeignKey("SiteCalling")]
        //public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

        [Column("geometry", TypeName = "geometry(MultiLineString,26710)")]
        public MultiLineString Geometry { get; set; }


    }
}
