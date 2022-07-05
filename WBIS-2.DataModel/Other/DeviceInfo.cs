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
    public class DeviceInfo: IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Column("site_calling_id"),ForeignKey("SiteCalling")]
        public Guid? SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

     

        [Column("owl_banding_id"), ForeignKey("OwlBanding")]
        public Guid? OwlBandingId { get; set; }
        public OwlBanding OwlBanding { get; set; }

        [Column("amphibian_survey_id"), ForeignKey("AmphibianSurvey")]
        public Guid? AmphibianSurveyId { get; set; }
        public AmphibianSurvey AmphibianSurvey { get; set; }
        [Column("amphibian_element_id"), ForeignKey("AmphibianElement")]
        public Guid? AmphibianElementId { get; set; }
        public AmphibianElement AmphibianElement { get; set; }
        [Column("botanical_element_id"), ForeignKey("BotanicalElement")]
        public Guid? BotanicalElementId { get; set; }
        public BotanicalElement BotanicalElement { get; set; }
        [Column("botanical_survey_id"), ForeignKey("BotanicalSurvey")]
        public Guid? BotanicalSurveyId { get; set; }
        public BotanicalSurvey BotanicalSurvey { get; set; }

        //[Column("forest_carnivore_camera_station_id"), ForeignKey("ForestCarnivoreCameraStation")]
        //public Guid? ForestCarnivoreCameraStationId { get; set; }
        //public ForestCarnivoreCameraStation ForestCarnivoreCameraStation { get; set; }

        //[Column("ranch_photo_point_id"), ForeignKey("RanchPhotoPoint")]
        //public Guid? RanchPhotoPointId { get; set; }
        //public RanchPhotoPoint RanchPhotoPoint { get; set; }

        //[Column("do_monitoring_id"), ForeignKey("DOMonitoring")]
        //public Guid? DOMonitoringId { get; set; }
        //public DOMonitoring DOMonitoring { get; set; }
        //[Column("bdow_sighting_id"), ForeignKey("BDOWSighting")]
        //public Guid? BDOWSightingId { get; set; }
        //public BDOWSighting BDOWSighting { get; set; }



        [Column("device_time")]
        public DateTime DeviceTime { get; set; }


        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        /// <summary>
        /// Device Lat
        /// </summary>
        [Column("lat")]
        public double Lat { get; set; }
        /// <summary>
        /// DEvice Lon
        /// </summary>
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }

        [Column("horizontal_accuracy")]
        public double HorizontalAccuracy { get; set; }
    }
}
