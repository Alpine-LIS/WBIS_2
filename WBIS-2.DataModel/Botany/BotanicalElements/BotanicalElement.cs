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
    public class BotanicalElement : UserDataValidator, IUserRecords, IQueryStuff<BotanicalElement>, IPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }



        [Column("record_type")]
        public string RecordType { get; set; }



        [Required, Column("botanical_scoping_id")]
        public Guid BotanicalScopingId { get; set; }
        public BotanicalScoping BotanicalScoping { get; set; }

        [Required, Column("botanical_survey_area_id")]
        public Guid BotanicalSurveyAreaId { get; set; }
        public BotanicalSurveyArea BotanicalSurveyArea { get; set; }

        [Required, Column("botanical_survey_id")]
        public Guid BotanicalSurveyId { get; set; }
        public BotanicalSurvey BotanicalSurvey { get; set; }

        [Column("botanical_point_of_interest_id")]
        public Guid BotanicalPointOfInterestId { get; set; }
        public BotanicalPointOfInterest BotanicalPointOfInterest { get; set; }

        [Column("botanical_plant_of_interest_id")]
        public Guid BotanicalPlantOfInterestId { get; set; }
        public BotanicalPlantOfInterest BotanicalPlantOfInterest { get; set; }
        
        [Column("botanical_plant_list_id")]
        public Guid BotanicalPlantListId { get; set; }
        public BotanicalPlantList BotanicalPlantList { get; set; }


        [Column("device_info_id")]
        public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }





        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Display(Order = -1)]
        public bool _delete { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;


        [Required, Column("date_time")]
        public DateTime DateTime { get; set; }       
        [Column("comments")]
        public string Comments { get; set; }






        [Column("district_id")]
        public Guid DistrictId { get; set; }
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }





        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Botanical Elements"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<BotanicalElement, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<BotanicalElement, bool>> a;

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
