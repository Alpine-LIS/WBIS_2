using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class CDFW_SpottedOwl : IInformationType
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }

        [Column("sname")]
        public string SNAME { get; set; }
        [Column("cname")]
        public string CNAME { get; set; }
        [Column("obsid")]
        public int OBSID { get; set; }
        [Column("masterowl")]
        public string MASTEROWL { get; set; }
        [Column("typeobs")]
        public string TYPEOBS { get; set; }
        [Column("observer")]
        public string OBSERVER { get; set; }
        [Column("dateobs")]
        public string DATEOBS { get; set; }
        [Column("timeobs")]
        public string TIMEOBS { get; set; }
        [Column("numadobs")]
        public int NUMADOBS { get; set; }
        [Column("agesex")]
        public string AGESEX { get; set; }
        [Column("pair")]
        public string PAIR { get; set; }
        [Column("nest")]
        public string NEST { get; set; }
        [Column("numyoung")]
        public string NUMYOUNG { get; set; }
        [Column("subspecies")]
        public string SUBSPECIES { get; set; }
        [Column("londd_n83")]
        public double LONDD_N83 { get; set; }
        [Column("latdd_n83")]
        public double LATDD_N83 { get; set; }
        [Column("coordsrc")]
        public string COORDSRC { get; set; }
        [Column("docid")]
        public string DOCID { get; set; }
        [Column("comments")]
        public string COMMENTS { get; set; }
        [Column("mtrs")]
        public string MTRS { get; set; }
        [Column("highestuse")]
        public string HIGHESTUSE { get; set; }
        [Column("symbology")]
        public string SYMBOLOGY { get; set; }


       
        [Column("geometry"), DataType("geometry(Point,26710)")]
        public Point Geometry { get; set; }

        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "CDFW Spotted OWl"; } }
    }
}
