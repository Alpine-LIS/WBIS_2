using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IPointLayer
    {
        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("starting_lat")]
        public double Lat { get; set; }
        [Column("starting_lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }
    }
}
