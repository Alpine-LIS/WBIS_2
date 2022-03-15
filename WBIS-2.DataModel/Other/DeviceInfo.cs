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
    public class DeviceInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Column("site_calling_id"),ForeignKey("SiteCalling")]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

        [Column("site_calling_repository_id"), ForeignKey("SiteCallingRepository")]
        public Guid SiteCallingRepositoryId { get; set; }
        public SiteCallingRepository SiteCallingRepository { get; set; }


        [Column("owl_banding_id"), ForeignKey("OwlBanding")]
        public Guid OwlBandingId { get; set; }
        public OwlBanding OwlBanding { get; set; }

        [Column("amphibian_survey_id"), ForeignKey("AmphibianSurvey")]
        public Guid AmphibianSurveyId { get; set; }
        public AmphibianSurvey AmphibianSurvey { get; set; }
        [Column("amphibian_element_id"), ForeignKey("AmphibianElement")]
        public Guid AmphibianElementId { get; set; }
        public AmphibianElement AmphibianElement { get; set; }
        [Column("botanical_element_id"), ForeignKey("BotanicalElement")]
        public Guid BotanicalElementId { get; set; }
        public BotanicalElement BotanicalElement { get; set; }



        [Column("device_time")]
        public DateTime DeviceTime { get; set; }
        
        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("device_lat")]
        public double DeviceLat { get; set; }
        [Column("device_lon")]
        public double DeviceLon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }

        [Column("horizontal_accuracy")]
        public double HorizontalAccuracy { get; set; }
    }
}
