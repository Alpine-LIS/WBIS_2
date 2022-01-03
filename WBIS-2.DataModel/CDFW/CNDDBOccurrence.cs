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
    public class CNDDBOccurrence : IInformationType
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }

        [Column("sname")]
        public string SNAME { get; set; }
        [Column("cname")]
        public string CNAME { get; set; }
        [Column("elmcode")]
        public string ELMCODE { get; set; }
        [Column("occnumber")]
        public int OCCNUMBER { get; set; }
        [Column("mapndx")]
        public string MAPNDX { get; set; }
        [Column("eondx")]
        public int EONDX { get; set; }
        [Column("keyquad")]
        public string KEYQUAD { get; set; }
        [Column("kquadname")]
        public string KQUADNAME { get; set; }
        [Column("keycounty")]
        public string KEYCOUNTY { get; set; }
        [Column("plss")]
        public string PLSS { get; set; }
        [Column("elevation")]
        public int ELEVATION { get; set; }
        [Column("parts")]
        public int PARTS { get; set; }
        [Column("elmtype")]
        public int ELMTYPE { get; set; }
        [Column("taxongroup")]
        public string TAXONGROUP { get; set; }
        [Column("eocount")]
        public int EOCOUNT { get; set; }
        [Column("accuracy")]
        public string ACCURACY { get; set; }
        [Column("presence")]
        public string PRESENCE { get; set; }
        [Column("occtype")]
        public string OCCTYPE { get; set; }
        [Column("occrank")]
        public string OCCRANK { get; set; }
        [Column("sensitive")]
        public string SENSITIVE { get; set; }
        [Column("sitedate")]
        public string SITEDATE { get; set; }
        [Column("elmdate")]
        public string ELMDATE { get; set; }
        [Column("ownermgt")]
        public string OWNERMGT { get; set; }
        [Column("fedlist")]
        public string FEDLIST { get; set; }
        [Column("callist")]
        public string CALLIST { get; set; }
        [Column("grank")]
        public string GRANK { get; set; }
        [Column("srank")]
        public string SRANK { get; set; }
        [Column("rplantrank")]
        public string RPLANTRANK { get; set; }
        [Column("cdfwstatus")]
        public string CDFWSTATUS { get; set; }
        [Column("othrstatus")]
        public string OTHRSTATUS { get; set; }
        [Column("location")]
        public string LOCATION { get; set; }
        [Column("locdetails")]
        public string LOCDETAILS { get; set; }
        [Column("ecological")]
        public string ECOLOGICAL { get; set; }
        [Column("general")]
        public string GENERAL { get; set; }
        [Column("threat")]
        public string THREAT { get; set; }
        [Column("threatlist")]
        public string THREATLIST { get; set; }
        [Column("lastupdate")]
        public string LASTUPDATE { get; set; }
        [Column("area")]
        public double AREA { get; set; }
        [Column("perimeter")]
        public double PERIMETER { get; set; }
        [Column("avlcode")]
        public int AVLCODE { get; set; }
        [Column("symbology")]
        public int Symbology { get; set; }
        [Column("symbology_text")]
        public string SymbologyText { get; set; }

       
        [Column("geometry"),DataType("geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }

        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "CNDDB Occurrence"; } }


        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<object, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<object, bool>> a;
            if (QueryType == typeof(District))
                a = _ => ((CNDDBOccurrence)_).Districts.Any(d => Query.Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => ((CNDDBOccurrence)_).Watersheds.Any(d => Query.Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => ((CNDDBOccurrence)_).Quad75s.Any(d => Query.Contains(d));
            else
                a = _ => Query.Contains(((CNDDBOccurrence)_));
            return a;
        }
    }
}
