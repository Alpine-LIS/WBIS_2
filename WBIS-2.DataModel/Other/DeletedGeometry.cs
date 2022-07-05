using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.DataModel
{
    public class DeletedGeometry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required,Column("object_guid")]
        public Guid ObjectGuid { get; set; }
        [Column("poly_geometry", TypeName = "geometry(Polygon,26710)")]
        public Polygon PolyGeometry { get; set; }
        [Column("mpoly_geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon MPolyGeometry { get; set; }
        [Column("point_geometry", TypeName = "geometry(Point,26710)")]
        public Point PointGeometry { get; set; }
        [Column("line_geometry", TypeName = "geometry(LineString,26710)")]
        public LineString LineGeometry { get; set; }
    }
}
