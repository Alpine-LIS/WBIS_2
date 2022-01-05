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
    public class CDFW_SpottedOwl : IInformationType, IQueryStuff<CDFW_SpottedOwl>
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

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<CDFW_SpottedOwl, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<CDFW_SpottedOwl, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else
                a = _ => Query.Contains(_);
            return a;
        }
        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
