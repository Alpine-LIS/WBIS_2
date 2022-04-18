using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class AmphibianSurvey : UserDataValidator, IUserRecords, INonPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

    

        public ICollection<AmphibianElement> AmphibianElements { get; set; }


        [Required,Column("site_id")]
        public string SiteID { get;set; }
        [Required, Column("surveyors")]
        public string Surveyors { get; set; }
        [Column("date_time")]
        public DateTime DateTime { get; set; }
        [Column("lake_stream_name")]
        public string LakeStreamName { get; set; }
        [Column("water_type")]
        public string WaterType { get; set; }
        [Column("seasonality_if_flow")]
        public string SeasonalityOfFlow { get; set; }
        [Column("planning_watershed")]
        public string PlanningWatershed { get; set; }
        [Column("county")]
        public string County { get; set; }
        [Column("elevation")]
        public string Elevation { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("weather")]
        public string Weather { get; set; }
        [Column("wind")]
        public string Wind { get; set; }
        [Column("location_comments")]
        public string LocationComments { get; set; }
        [Column("canopy_closure")]
        public double CanopyClosure { get; set; }
        [Column("stream_gradient")]
        public string StreamGradient { get; set; }
        [Column("silt")]
        public double Silt { get; set; }
        [Column("sand")]
        public double Sand { get; set; }
        [Column("gravel")]
        public double Gravel { get; set; }
        [Column("cobble")]
        public double Cobble { get; set; }
        [Column("boulders")]
        public double Boulders { get; set; }
        [Column("bedrock")]
        public double Bedrock { get; set; }
        [Column("pool")]
        public double Pool { get; set; }
        [Column("riffle")]
        public double Riffle { get; set; }
        [Column("run")]
        public double Run { get; set; }
        [Column("est_avg_stream_width")]
        public double EstAvgStreamWidth { get; set; }
        [Column("water_temp")]
        public double WaterTemp { get; set; }
        [Column("air_temp")]
        public double AirTemp { get; set; }
        [Column("flow")]
        public string Flow { get; set; }




        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }




        [Column("user_id")]
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }






        [Required, Column("geometry", TypeName = "geometry(LineString,26710)")]
        public LineString Geometry { get; set; }



        //[Column("device_info_id")]
        //public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }






        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }





        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new AmphibianSurveyManager(); } }
    }
}
