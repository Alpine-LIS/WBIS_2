using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("quad75s")]
    [DisplayOrder(Index = 1), TypeGrouper(IgnoreGroups = true)]
    public class Quad75 : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required, Column("quad_code"), ListInfo(DisplayField = true)]
        public string QuadCode { get; set; }
        [Required, Column("isgs_code")]
        public string UsgsCode { get; set; }
        [Column("cnps_code")]
        public string CNPSCode { get; set; }
        [Column("quad_name"), ListInfo(DisplayField = true)]
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
        [ListInfo(ChildField = true)]
        public ICollection<Hex160> Hex160s { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<Watershed> Watersheds { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<SiteCalling> SiteCallings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SiteCallingDetection> SiteCallingDetections { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<OwlBanding> OwlBandings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<AmphibianElement> AmphibianElements { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalElement> BotanicalElements { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<CNDDBQuadElement> CNDDBQuadElements { get; set; }



        //[ListInfo(ChildField = true)]
        //public ICollection<ForestCarnivoreCameraStation> ForestCarnivoreCameraStations { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<RanchPhotoPoint> RanchPhotoPoints { get; set; }

        //[ListInfo(ChildField = true)]
        //public ICollection<DOMonitoring> DOMonitorings { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<BDOWSighting> BDOWSightings { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<Quad75>();
    }
}
