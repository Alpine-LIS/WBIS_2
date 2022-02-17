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
    public class SiteCalling : UserDataValidator, IUserRecords, IQueryStuff<SiteCalling>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }

        [Required, Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;


        [Column("starting_location"), DataType("geometry(Point,26710)")]
        public Point StartingLocation { get; set; }
        [Column("starting_lat")]
        public double StartingLat { get; set; }
        [Column("starting_lon")]
        public double StartingLon { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        [Column("end_time")]
        public DateTime EndTime { get; set; }
        [Column("sunset_time")]
        public DateTime SunsetTime { get; set; }

        [Column("detection_type")]
        public string DetectionType { get; set; }
        [Column("survey_type1")]
        public string SurveyType1 { get; set; }
        [Column("survey_type2")]
        public string SurveyType2 { get; set; }
        [Required, Column("bird_species_survey_id")]
        public Guid SurveySpeciesId { get; set; }
        public BirdSpecies SurveySpecies { get; set; }
        [Column("site_type")]
        public string SiteType { get; set; }
        [Column("site_id")]
        public string SiteID { get; set; }
        [Column("pass_number")]
        public int PassNumber { get; set; }
        [Column("preotection_zone_id")]
        public Guid ProtectionZoneID { get; set; }
        public ProtectionZone ProtectionZone { get; set; }
        [Column("yearly_activity_center")]
        public bool YearlyActivityCenter { get; set; }
        [Column("wind")]
        public string Wind { get; set; }
        [Column("precipitation")]
        public string Precipitation { get; set; }










        [Column("target_species_present")]
        public bool TargetSpeciesPresent { get; set; }
        [Column("site_calling_detection_id")]
        public Guid SiteCallingDetectionID { get; set; }
        public SiteCallingDetection SiteCallingDetection { get; set; }


        [Column("occupancy_status")]
        public string OccupancyStatus { get; set; }
        [Column("nesting_status")]
        public string NestingStatus { get; set; }
        [Column("reproductive_status")]
        public string ReproductiveStatus { get; set; }
        [Column("nest_tree")]
        public bool NestTree { get; set; }
        [Column("nest_type")]
        public string NestType { get; set; }
        [Column("tree_species")]
        public string TreeSpecies { get; set; }
        [Column("dbh")]
        public double DBH { get; set; }
        [Column("nest_height")]
        public double NestHeight { get; set; }
        [Column("tree_tagged")]
        public bool TreeTagged { get; set; }
        [Column("moused")]
        public bool Moused { get; set; }



        [Column("area_description")]
        public string AreaDescription { get; set; }
        [Column("comments")]
        public string Comments { get; set; }



        [Column("site_calling_track_id")]
        public Guid SiteCallingTrackID { get; set; }
        public SiteCallingTrack SiteCallingTrack { get; set; }
        [Column("site_calling_device_info_id")]
        public Guid SiteCallingDeviceInfoID { get; set; }
        public SiteCallingDeviceInfo SiteCallingDeviceInfo { get; set; }






        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
       
       





        public ICollection<OtherWildlife> OtherWildlifeRecords { get; set; }
        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Site Calling"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<SiteCalling, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<SiteCalling, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Hex160.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Hex160.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Hex160.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
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
