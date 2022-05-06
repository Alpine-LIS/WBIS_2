using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WBIS_2.DataModel
{
    public class Hex160 : IInformationType, IActiveUnit
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("hex160_id"), ListInfo(DisplayField = true)]
        public string Hex160ID { get; set; }
        [Column("calling_responses")]
        public int CallingResponses { get; set; }
        [Column("follow_ups")]
        public int FollowUps { get; set; }
        [Column("skips")]
        public int Skips { get; set; }
        [Column("drops")]
        public int Drops { get; set; }
        [Column("recent_activity")]
        public string RecentActivity { get; set; }
        [Column("latest_activity")]
        public DateTime? LatestActivity { get; set; }


        [Column("is_active")]
        public bool IsActive { get; set; }
        [ListInfo(AutoInclude = true)]
        public ICollection<ApplicationUser> ActiveUsers { get; set; }

        [Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }

        [Column("current_protection_zone_id"), ForeignKey("ProtectionZone")]
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
       
        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<Hex160>();
    }
}
