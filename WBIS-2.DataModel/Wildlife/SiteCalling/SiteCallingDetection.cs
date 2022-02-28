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
    public class SiteCallingDetection
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("site_calling_id")]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }


        [Required, Column("detection_time")]
        public DateTime DetectionTime { get; set; }

        
        [Required, Column("bird_species_found_id")]
        public Guid SpeciesFoundId { get; set; }
        public BirdSpecies SpeciesFound { get; set; }
        [Required, Column("detection_method")]
        public string DetectionMethod { get; set; }

        [Required, Column("geometry"), DataType("geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Required, Column("detection_lat")]
        public double DetectionLat { get; set; }
        [Required, Column("detection_lon")]
        public double DetectionLon { get; set; }


        [Column("user_location_id")]
        public Guid UserLocationId { get; set; }
        public UserLocation UserLocation { get; set; }


        [Required, Column("distance")]
        public double Distance { get; set; }
        [Required, Column("bearing")]
        public double Bearing { get; set; }
        [Required, Column("estimated_location")]
        public bool EstimatedLocation { get; set; } = false;
        [Required, Column("sex")]
        public string Sex { get; set; }
        [Required, Column("age")]
        public string Age { get; set; }
        [Column("number_of_young")]
        public int NumberOfYoung { get; set; }
        [Column("species_site")]
        public string SpeciesSite { get; set; }
        [Column("male_banding_leg")]
        public string MaleBindingLeg { get; set; }
        [Column("male_banding_pattern")]
        public string MaleBindingPattern { get; set; }
        [Column("female_banding_leg")]
        public string FemaleBindingLeg { get; set; }
        [Column("female_banding_pattern")]
        public string FemaleBindingPattern { get; set; }     
    }
}
