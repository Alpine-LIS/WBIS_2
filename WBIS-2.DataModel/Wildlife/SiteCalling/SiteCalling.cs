using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = 14), TypeGrouper(GroupName = "Wildlife"), GeometryEdits(Locked = false)]
    public class SiteCalling : UserDataValidator, IUserRecords,  IPointParents, IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

       

        [Column("user_id")]
        public Guid? UserId { get; set; }
        [ImportAttribute(Required = true), ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }

        [Required, Column("record_type"), ImportAttribute(Required = true)]
        public string RecordType { get; set; }


        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        /// <summary>
        /// Starting Lat
        /// </summary>
        [Column("lat")]
        public double Lat { get; set; }
        /// <summary>
        /// Starting Lon
        /// </summary>
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }
        [Required, Column("start_time"), ImportAttribute(Required = true)]
        public DateTime StartTime { get; set; }
        [Required, Column("end_time"), ImportAttribute]
        public DateTime EndTime { get; set; }
        [Column("sunset_time")]
        public DateTime? SunsetTime { get; set; }

       
        [Required, Column("survey_type1"), ImportAttribute(Required = true)]
        public string SurveyType1 { get; set; }
        [Required, Column("survey_type2"), ImportAttribute(Required = true)]
        public string SurveyType2 { get; set; }
        [Required, Column("bird_species_survey_id")]
        public Guid SurveySpeciesId { get; set; }
        [ImportAttribute(Required = true), ListInfo(AutoInclude = true)]
        public BirdSpecies SurveySpecies { get; set; }
       
       //site type is survey type1
        [Column("site_id"), ImportAttribute]
        public string SiteID { get; set; }
        
        [Column("pass_number"), ImportAttribute]
        public int PassNumber { get; set; }
        [Column("manual_pass_changed"), Display(Order = -1)]
        public bool ManualPassChanged { get; set; } = false;

        [Column("pz_pass_number"), ImportAttribute]
        public int PZPassNumber { get; set; }

        //Initially gotten by the hex pz. Can be edited. Must be retained when pz changes. 
        [Column("protection_zone_id")]
        public Guid? ProtectionZoneID { get; set; }
        [ImportAttribute, ListInfo(AutoInclude = true)]
        public ProtectionZone ProtectionZone { get; set; }



        [Column("yearly_activity_center"), ImportAttribute]
        public bool YearlyActivityCenter { get; set; }
        [Column("wind"), ImportAttribute]
        public string Wind { get; set; }
        [Column("precipitation"), ImportAttribute]
        public string Precipitation { get; set; }









        [Column("species_present"), ImportAttribute]
        public bool SpeciesPresent { get; set; }
        [Column("target_species_present"), ImportAttribute]
        public bool TargetSpeciesPresent { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<SiteCallingDetection> SiteCallingDetections { get; set;}
        //[Column("site_calling_detection_id")]
        //public Guid SiteCallingDetectionID { get; set; }
        //public SiteCallingDetection SiteCallingDetection { get; set; }


        //Following three come from tables but no link is established. Kevin Roberts changes these more regularly so old options are not maintained but string value remaines. 
        [Required, Column("spow_occupancy_status"), ImportAttribute]
        public string SPOW_OccupancyStatus { get; set; }
        [Column("nesting_status"), ImportAttribute]
        public string NestingStatus { get; set; }
        [Column("reproductive_status"), ImportAttribute]
        public string ReproductiveStatus { get; set; }



        [Column("nest_tree"), ImportAttribute]
        public bool NestTree { get; set; }
        [Column("nest_type"), ImportAttribute]
        public string NestType { get; set; }
        [Column("tree_species"), ImportAttribute]
        public string TreeSpecies { get; set; }
        [Column("dbh"), ImportAttribute]
        public double DBH { get; set; }
        [Column("nest_height"), ImportAttribute]
        public double NestHeight { get; set; }
        [Column("tree_tagged"), ImportAttribute]
        public bool TreeTagged { get; set; }
        


        [Column("area_description"), ImportAttribute]
        public string AreaDescription { get; set; }
        [Column("comments"), ImportAttribute]
        public string Comments { get; set; }



        //[Column("site_calling_track_id")]
        //public Guid SiteCallingTrackID { get; set; }
        public SiteCallingTrack SiteCallingTrack { get; set; }
        //[Column("device_info_id")]
        //public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }






        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }




        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid? Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid? Hex160Id { get; set; }
        [ListInfo(AutoInclude = true)]
        public Hex160 Hex160 { get; set; }

        [Column("hex500_id")]
        public Guid? Hex500Id { get; set; }
        [ListInfo(AutoInclude = true)]
        public Hex500 Hex500 { get; set; }





        [ListInfo(ChildField = true)]
        public ICollection<OtherWildlife> OtherWildlifeRecords { get; set; }
        public ICollection<Picture> Pictures { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new InformationTypeManager<SiteCalling>(); } }
    }
}
