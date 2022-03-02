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
    public interface ISiteCalling : IUserRecords, IQueryStuff<ISiteCalling>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }





        [Required, Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Required, Column("starting_lat")]
        public double StartingLat { get; set; }
        [Required, Column("starting_lon")]
        public double StartingLon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }
        [Required, Column("start_time")]
        public DateTime StartTime { get; set; }
        [Required, Column("end_time")]
        public DateTime EndTime { get; set; }
        [Required, Column("sunset_time")]
        public DateTime SunsetTime { get; set; }

       
        [Required, Column("survey_type1")]
        public string SurveyType1 { get; set; }
        [Required, Column("survey_type2")]
        public string SurveyType2 { get; set; }
        [Required, Column("bird_species_survey_id")]
        public Guid SurveySpeciesId { get; set; }
        public BirdSpecies SurveySpecies { get; set; }
       
       //site type is survey type1
        [Column("site_id")]
        public string SiteID { get; set; }
        
        [Column("pass_number")]
        public int PassNumber { get; set; }

        [Column("pz_pass_number")]
        public int PZPassNumber { get; set; }

        //Initially gotten by the hex pz. Can be edited. Must be retained when pz changes. 
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


        [Required, Column("occupancy_status")]
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



        [Column("device_info_id")]
        public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }






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

        public abstract Expression<Func<ISiteCalling, bool>> GetParentWhere(object[] Query, Type QueryType);

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
