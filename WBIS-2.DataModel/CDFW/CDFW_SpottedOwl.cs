using Microsoft.EntityFrameworkCore;
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
    public class CDFW_SpottedOwl : IInformationType, IQueryStuff, IPointParents
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


       
        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }


        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid? Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid? Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "CDFW Spotted OWl"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<CDFW_SpottedOwl>();
            var a = (Expression<Func<CDFW_SpottedOwl, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                return returnVal.Include(_ => _.District).Where(a);
            else if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watershed).Where(a);
            else if (QueryType == typeof(Quad75))
                return returnVal.Include(_ => _.Quad75).Where(a);
            else if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160).Where(a);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<CDFW_SpottedOwl, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => Query.Cast<Watershed>().Contains(_.Watershed);
            else if (QueryType == typeof(Quad75))
                a = _ => Query.Cast<Quad75>().Contains(_.Quad75);
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
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
