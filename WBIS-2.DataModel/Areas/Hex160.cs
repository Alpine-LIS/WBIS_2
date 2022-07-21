using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("hex160s")]
    [DisplayOrder(Index = 3), TypeGrouper(IgnoreGroups = true), ReportableTable]
    public class Hex160 : IInformationType, IActiveUnit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//, Column("id")]
        public Guid Id { get; set; }
        [Required, Column("hex160_id"), ListInfo(DisplayField = true)]
        public string Hex160ID { get; set; }
        public int CallingResponses { get; set; }
        public int FollowUps { get; set; }
        public int Skips { get; set; }
        public int Drops { get; set; }
        public string RecentActivity { get; set; }
        public DateTime? LatestActivity { get; set; }


        //if (CanSetActive) query = query.Replace($"\"is_active\"", $"guid IN (SELECT unit_id FROM active_{et.GetSchemaQualifiedTableName()} WHERE application_user_id = '{guid}') as \"is_active\"");
        [ ActiveRecordQuery("active_hex160s", "id", "unit_id", "application_user_id", typeof(CurrentUser), "UsingGuid")]
        public bool IsActive { get; set; }
        [ListInfo(AutoInclude = true)]
        public ICollection<ApplicationUser> ActiveUsers { get; set; }

        [Column(TypeName = "geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }

        [ForeignKey("ProtectionZone")]
        public Guid? CurrentProtectionZoneID { get; set; }
        [ListInfo(AutoInclude = true)]
        public ProtectionZone CurrentProtectionZone { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<ProtectionZone> ProtectionZones { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<PermanentCallStation> PermanentCallStations { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SiteCalling> SiteCallings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SiteCallingDetection> SiteCallingDetections { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<OwlBanding> OwlBandings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<AmphibianElement> AmphibianElements { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }


        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalElement> BotanicalElements { get; set; }


        //[ListInfo(ChildField = true)]
        //public ICollection<ForestCarnivoreCameraStation> ForestCarnivoreCameraStations { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<RanchPhotoPoint> RanchPhotoPoints { get; set; }

        //[ListInfo(ChildField = true)]
        //public ICollection<DOMonitoring> DOMonitorings { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<BDOWSighting> BDOWSightings { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<Hex160>();
    }
}
