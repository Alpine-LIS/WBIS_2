using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WBIS_2.DataModel
{
    public class Quad75 : IInformationType, IQueryStuff//<Quad75>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
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



        [Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }


        public ICollection<District> Districts { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }

        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<OwlBanding> OwlBandings { get; set; }
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get; set; }
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        public ICollection<AmphibianElement> AmphibianElements { get; set; }

        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        public ICollection<BotanicalElement> BotanicalElements { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Quad75"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new Hex160(), new CNDDBOccurrence(), new CDFW_SpottedOwl(), new SiteCalling(), new OwlBanding(), new SPIPlantPolygon(),
                new SPIPlantPoint(), new AmphibianSurvey(), new AmphibianElement(), new BotanicalScoping(), new BotanicalSurveyArea(), new BotanicalSurvey(), new BotanicalElement()
            }; }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<Quad75>();
            var a = (Expression<Func<Quad75, bool>>)GetParentWhere(Query, QueryType);

            List<KeyValuePair<string, string>> v = Hex160.DisplayFields;

            if (QueryType == typeof(District))
                return returnVal.Include(_ => _.Districts).Where(a);
            else if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160s).Where(a);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Quad75, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public static List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("QuadCode", "Quad75")};
            }
        }
    }

    public class Quad75Manager
    {

    }
}
