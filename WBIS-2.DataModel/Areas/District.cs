using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("districts")]
    [DisplayOrder(Index = 0), TypeGrouper(IgnoreGroups = true)]
    public class District : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required, Column("district_name"), ListInfo(DisplayField = true)]
        public string DistrictName { get; set; }
        [Required, Column("management_area"), ListInfo(DisplayField = true)]
        public string ManagementArea { get; set; }



        //[Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        //public MultiPolygon Geometry { get; set; }

        //[Column("district_extended_geometry_id")]
        //public Guid DistrictExtendedGeometryID { get; set; }
        [Display(AutoGenerateField = false)]
        public DistrictExtendedGeometry DistrictExtendedGeometry { get; set; }

        public ICollection<CDFW_SpottedOwlDiagram> CDFW_SpottedOwlDiagrams { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }



        [ListInfo(ChildField = true)]
        public ICollection<Hex160> Hex160s { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<Quad75> Quad75s { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<Watershed> Watersheds { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }        
        [ListInfo(ChildField = true)]
        public ICollection<OwlBanding> OwlBandings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SiteCalling> SiteCallings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SiteCallingDetection> SiteCallingDetections { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get; set; }
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
        public ICollection<PlantProtectionSummary> PlantProtectionSummaries { get; set; }

        //[ListInfo(ChildField = true)]
        //public ICollection<ForestCarnivoreCameraStation> ForestCarnivoreCameraStations { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<RanchPhotoPoint> RanchPhotoPoints { get; set; }

        //[ListInfo(ChildField = true)]
        //public ICollection<DOMonitoring> DOMonitorings { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<BDOWSighting> BDOWSightings { get; set; }


        public ICollection<SPI_GGOW> SPI_GGOWs { get; set; }
        public ICollection<SPI_SPOW> SPI_SPOWs { get; set; }
        public ICollection<SPI_NOGO> SPI_NOGOs { get; set; }
        public ICollection<SPI_WildlifeSighting> SPI_WildlifeSightings { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<District>();
    }
}
