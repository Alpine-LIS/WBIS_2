using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class Quad75 : IInformationType
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
        public IInfoTypeManager Manager { get { return new Quad75Manager(); } }
    }
}
