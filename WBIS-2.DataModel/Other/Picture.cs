using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.DataModel
{
    public class Picture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Column("date_time")]
        public DateTime DateTime { get; set; }
        [Column("image_data")]
        public byte[] ImageData { get; set; }
        [Column("preview_data")]
        public byte[] PreviewData { get; set; }

        [Column("site_calling_id")]
        public Guid? SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }
        [Column("owl_banding_id")]
        public Guid? OwlBandingId { get; set; }
        public OwlBanding OwlBanding { get; set; }
        [Column("botanical_element_id")]
        public Guid? BotanicalElementId { get; set; }
        public BotanicalElement BotanicalElement { get; set; }
        [Column("amphibian_element_id")]
        public Guid? AmphibianElementId { get; set; }
        public AmphibianElement AmphibianElement { get; set; }


        [Column("forest_carnivore_camera_station_id"), ForeignKey("ForestCarnivoreCameraStation")]
        public Guid? ForestCarnivoreCameraStationId { get; set; }
        public ForestCarnivoreCameraStation ForestCarnivoreCameraStation { get; set; }

        [Column("ranch_photo_point_id"), ForeignKey("RanchPhotoPoint")]
        public Guid? RanchPhotoPointId { get; set; }
        public RanchPhotoPoint RanchPhotoPoint { get; set; }

        [Column("do_monitoring_id"), ForeignKey("DOMonitoring")]
        public Guid? DOMonitoringId { get; set; }
        public DOMonitoring DOMonitoring { get; set; }

        [Column("bdow_sighting_id"), ForeignKey("BDOWSighting")]
        public Guid? BDOWSightingId { get; set; }
        public BDOWSighting BDOWSighting { get; set; }
    }
}
