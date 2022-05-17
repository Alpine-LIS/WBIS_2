using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalElement : UserDataValidator, IUserRecords, IPointParents, IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }



        [Column("record_type")]
        public string RecordType { get; set; }



        [ Column("botanical_scoping_id")]
        public Guid? BotanicalScopingId { get; set; }
        [ListInfo(ParentField = true)]
        public BotanicalScoping BotanicalScoping { get; set; }

        [Column("botanical_survey_area_id")]
        public Guid? BotanicalSurveyAreaId { get; set; }
        [ListInfo(ParentField = true)]
        public BotanicalSurveyArea BotanicalSurveyArea { get; set; }

        [Column("botanical_survey_id")]
        public Guid? BotanicalSurveyId { get; set; }
        [ListInfo(AutoInclude = true, ParentField = true)]
        public BotanicalSurvey BotanicalSurvey { get; set; }

        //[Column("botanical_point_of_interest_id")]
        //public Guid BotanicalPointOfInterestId { get; set; }
        public BotanicalPointOfInterest BotanicalPointOfInterest { get; set; }

        //[Column("botanical_plant_of_interest_id")]
        //public Guid BotanicalPlantOfInterestId { get; set; }
        public BotanicalPlantOfInterest BotanicalPlantOfInterest { get; set; }
        
        //[Column("botanical_plant_list_id")]
        //public Guid BotanicalPlantListId { get; set; }
        public BotanicalPlantList BotanicalPlantList { get; set; }


        //[Column("device_info_id")]
        //public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }

        [Column("photo_tag")]
        public string PhotoTag { get; set; }


        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        //[Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }





        [Column("user_id")]
        public Guid? UserId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }





       






        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid? Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid? Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }


        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }


        public ICollection<Picture> Pictures { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<BotanicalElement>();
    }
}
