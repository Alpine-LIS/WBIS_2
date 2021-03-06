using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("cdfw_spotted_owl_diagrams")]
    public class CDFW_SpottedOwlDiagram
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Column("geometry", TypeName = "geometry(LineString,26710)")]
        public LineString Geometry { get; set; }
        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
    }
}
