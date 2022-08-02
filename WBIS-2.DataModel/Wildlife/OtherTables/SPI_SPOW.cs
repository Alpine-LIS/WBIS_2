using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("spi_spows")]
    [TypeGrouper(GroupName = "Wildlife")]
    public class SPI_SPOW : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }




        [Column("uid"), Import]
        public int UID { get; set; }
        [Column("dist_id"), Import]
        public string Dist_ID { get; set; }
        [Column("cdfw_id"), Import]
        public string CDFW_ID { get; set; }
        [Column("territory"), Import]
        public string Territory { get; set; }
        [Column("year"), Import]
        public int Year { get; set; }
        [Column("bird_status"), Import]
        public string BirdStatus { get; set; }
        [Column("hcp_status"), Import]
        public string HCP_Status { get; set; }
        [Column("hcp_status_2"), Import]
        public string HCP_Status_2 { get; set; }
        [Column("occupied"), Import]
        public string Occupied { get; set; }
        [Column("num_seen"), Import]
        public int NumSeen { get; set; }
        [Column("adult_subadult_count"), Import]
        public int AdultSubadultCount { get; set; }
        [Column("pair"), Import]
        public string Pair { get; set; }
        [Column("nest"), Import]
        public string Nest { get; set; }
        [Column("young"), Import]
        public string Young { get; set; }
        [Column("number_of_young"), Import]
        public int NumberOfYoung { get; set; }
        [Column("yac"), Import]
        public string YAC { get; set; }
        [Column("response"), Import]
        public string Response { get; set; }
        [Column("male_response"), Import]
        public string MaleResponse { get; set; }
        [Column("female_response"), Import]
        public string FemaleResponse { get; set; }
        [Column("unknown_sex_response"), Import]
        public string UnknownSexResponse { get; set; }
        [Column("barred_owl_response"), Import]
        public string BarredOwlResponse { get; set; }
        [Column("habitat_cross_plot"), Import]
        public string HabitatCrossPlot { get; set; }
        [Column("study_area"), Import]
        public string StudyArea { get; set; }
        [Column("unique_id"), Import]
        public int UniqueID { get; set; }
        [Column("latitude"), Import]
        public double Latitude { get; set; }
        [Column("longitude"), Import]
        public double Longitude { get; set; }
        [Column("township"), Import]
        public string Township { get; set; }
        [Column("range"), Import]
        public string Range { get; set; }
        [Column("section"), Import]
        public string Section { get; set; }
        [Column("quarter"), Import]
        public string Quarter { get; set; }
        [Column("sixteenth"), Import]
        public string Sixteenth { get; set; }
        [Column("source"), Import]
        public string Source { get; set; }
        [Column("sub_species1"), Import]
        public string SubSpecies1 { get; set; }
        [Column("age_sex"), Import]
        public string AgeSex { get; set; }
        [Column("visit_type"), Import]
        public string VisitType { get; set; }
        [Column("detection_type"), Import]
        public string DetectionType { get; set; }
        [Column("observer"), Import]
        public string Observer { get; set; }
        [Column("notes"), Import]
        public string Notes { get; set; }
        [Column("ws_id"), Import]
        public int WS_ID { get; set; }
        [Column("utm_northing_coordinate"), Import]
        public int UTM_NorthingCoordinate { get; set; }
        [Column("utm_easting_coordinate"), Import]
        public int UTM_EastingCoordinate { get; set; }
        [Column("wbis_mapped_location"), Import]
        public string WBIS_MappedLocation { get; set; }
        [Column("hex500_id"), Import]
        public string HEX_500_ID { get; set; }









        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true), Import]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        [ListInfo(AutoInclude = true), Import]
        public Watershed Watershed { get; set; }


        public bool _delete { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_SPOW>();
    }
}
