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
    public class AmphibianElement : UserDataValidator, IUserRecords, IQueryStuff<AmphibianElement>, IPointParents, IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }



        [Column("record_type")]
        public string RecordType { get; set; }




        [Required, Column("amphibian_survey_id")]
        public Guid AmphibianSurveyId { get; set; }
        public AmphibianSurvey AmphibianSurvey { get; set; }

        [Column("amphibian_location_found_id")]
        public Guid AmphibianLocationFoundId { get; set; }
        public AmphibianLocationFound AmphibianLocationFound { get; set; }

        [Column("amphibian_point_of_interest_id")]
        public Guid AmphibianPointOfInterestId { get; set; }
        public AmphibianPointOfInterest AmphibianPointOfInterest { get; set; }


        [Column("device_info_id")]
        public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }





        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }




        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }





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



        [Required, Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }

        public ICollection<Picture> Pictures { get; set; }




        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Amphibian Elements"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<AmphibianElement, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<AmphibianElement, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => Query.Cast<Watershed>().Contains(_.Watershed);
            else if (QueryType == typeof(Quad75))
                a = _ => Query.Cast<Quad75>().Contains(_.Quad75);
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            else if (QueryType == typeof(AmphibianSurvey))
                a = _ => Query.Cast<AmphibianSurvey>().Contains(_.AmphibianSurvey);
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
