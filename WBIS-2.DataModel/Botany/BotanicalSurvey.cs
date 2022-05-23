using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalSurvey : UserDataValidator, IUserRecords, INonPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


        [Column("survey_type")]
        public string SurveyType { get; set; }

        [Column("botanical_survey_area_id")]
        public Guid? BotanicalSurveyAreaId { get; set; }
        [ListInfo(ParentField = true, AutoInclude =true), Import(Required = true)]
        public BotanicalSurveyArea BotanicalSurveyArea { get; set; }

        [Column("botanical_scoping_id")]
        public Guid? BotanicalScopingId { get; set; }
        [ListInfo(ParentField = true)]
        public BotanicalScoping BotanicalScoping { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<BotanicalElement> BotanicalElement { get; set; }


        //[Column("survey_name")]
        //public string SurveyName { get; set; }
        [Column("other_surveyors"), Import()]
        public string OtherSurveyors { get; set; }
        [Column("start_time"), Import(Required = true)]
        public DateTime StartTime { get; set; }
        [Column("end_time"), Import(Required = true)]
        public DateTime EndTime { get; set; }

        [Column("time_spent")]
        public TimeSpan TimeSpent { get; set; }

        [Column("manual_track")]
        public bool ManualTrack { get; set; } = false;





        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        //[Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }

        [Column("comments")]
        public string Comments { get; set; }



        [Column("user_id")]
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }





        [Column("geometry", TypeName = "geometry(MultiLineString,26710)")]
        public MultiLineString Geometry { get; set; }



        //[Column("device_info_id")]
        //public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }






        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }


        [Column("thp_area_id")]
        public Guid? THP_AreaId { get; set; }
        [ListInfo(AutoInclude = true, DisplayField = true)]
        public THP_Area THP_Area { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<BotanicalSurvey>();
    }
}
