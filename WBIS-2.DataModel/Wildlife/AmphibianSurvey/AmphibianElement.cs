using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = 21), TypeGrouper(GroupName = "Wildlife"), GeometryEdits(Locked = false)]
    public class AmphibianElement : UserDataValidator, IUserRecords, IPointParents, IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }



        [Column("record_type")]
        public string RecordType { get; set; }




        [Column("amphibian_survey_id")]
        public Guid? AmphibianSurveyId { get; set; }
        [ListInfo(ParentField = true)]
        public AmphibianSurvey AmphibianSurvey { get; set; }

        //[Column("amphibian_location_found_id")]
        //public Guid AmphibianLocationFoundId { get; set; }
        [ListInfo(AutoInclude = true)]
        public AmphibianLocationFound AmphibianLocationFound { get; set; }

        //[Column("amphibian_point_of_interest_id")]
        //public Guid AmphibianPointOfInterestId { get; set; }
        [ListInfo(AutoInclude = true)]
        public AmphibianPointOfInterest AmphibianPointOfInterest { get; set; }


        //[Column("device_info_id")]
        //public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }





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





        [Required, Column("date_time")]
        public DateTime DateTime { get; set; }       
        [Column("comments")]
        public string Comments { get; set; }






        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
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
        public IInfoTypeManager Manager { get { return new InformationTypeManager<AmphibianElement>(); } }
    }
}
