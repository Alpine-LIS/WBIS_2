using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = 15)]
    public class SiteCallingDetection: UserDataValidator, IPointParents, IPointLayer, IUserRecords, IWildlifeRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Column("site_calling_id")]
        public Guid? SiteCallingId { get; set; }
        [ListInfo(AutoInclude = true, ParentField =true)]
        public SiteCalling SiteCalling { get; set; }


        [Required, Column("detection_time")]
        public DateTime DetectionTime { get; set; }

        
        [Required, Column("bird_species_found_id")]
        public Guid SpeciesFoundId { get; set; }
        public BirdSpecies SpeciesFound { get; set; }
        [Required, Column("detection_method")]
        public string DetectionMethod { get; set; }



        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        /// <summary>
        /// Detection Lat
        /// </summary>
        [Column("lat")]
        public double Lat { get; set; }
        /// <summary>
        /// Detection Lon
        /// </summary>
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }




        [Column("user_location_id")]
        public Guid? UserLocationId { get; set; }
        [ListInfo(AutoInclude =true)]
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
        public string MaleBandingLeg { get; set; }
        [Column("male_banding_pattern")]
        public string MaleBandingPattern { get; set; }
        [Column("female_banding_leg")]
        public string FemaleBandingLeg { get; set; }
        [Column("female_banding_pattern")]
        public string FemaleBandingPattern { get; set; }

        [Column("moused"), ImportAttribute]
        public bool Moused { get; set; }


        [Column("user_id")]
        public Guid? UserId { get; set; }
        [ImportAttribute(Required = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ImportAttribute(Required = true)]
        public ApplicationUser UserModified { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }



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
        [ListInfo(AutoInclude = true)]
        public Hex160 Hex160 { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new InformationTypeManager<SiteCallingDetection>(); } }
    }
}
