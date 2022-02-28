using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class CDFW_SpottedOwlDiagram
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }
       
        [Column("geometry"), DataType("geometry(LineString,26710)")]
        public LineString Geometry { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
