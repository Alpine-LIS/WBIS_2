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
    public class Quad75 : IInformationType, IQueryStuff<Quad75>
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity),Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("quad_code")]
        public string QuadCode { get; set; }
        [Required, Column("isgs_code")]
        public string UsgsCode { get; set; }
        [Column("cnps_code")]
        public string CNPSCode { get; set; }
        [Column("quad_name")]
        public string QuadName { get; set; }
        [Column("quad_size")]
        public string QuadSize { get; set; }
        [Column("q24_year")]
        public int Q24Year { get; set; }
        [Column("q100_name")]
        public string Q100Name { get; set; }
        [Column("border")]
        public string Border { get; set; }
        [Column("utm_zone")]
        public string UTMZone { get; set; }
        [Column("b_m")]
        public string B_M { get; set; }
        [Column("sensitive")]
        public string Sensitive { get; set; }
        [Column("perimeter")]
        public double Perimeter { get; set; }
        [Column("area")]
        public double Area { get; set; }



        [Column("geometry"), DataType("geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }


        public ICollection<Hex160> Hex160s { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }

        


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Quad75"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new Hex160(), new SiteCalling(), new CNDDBOccurrence(), new CDFW_SpottedOwl() }; }
        }
        public Expression<Func<Quad75, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Quad75, bool>> a = _ => Query.Contains((_));
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("QuadCode", "Quad75")};
            }
        }
    }
}
