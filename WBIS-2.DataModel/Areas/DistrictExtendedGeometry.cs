using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    [Table("district_extended_geometries")]
    public class DistrictExtendedGeometry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id"), ForeignKey("District")]
        public Guid Id { get; set; }


        //[Required, Column("district_id"),ForeignKey("District")]
        //public Guid DistrictId { get; set; }
        public District District { get; set; }


        [Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }
    }
}
