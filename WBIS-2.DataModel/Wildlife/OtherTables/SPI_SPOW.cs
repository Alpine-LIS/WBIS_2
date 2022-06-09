using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [TypeGrouper(GroupName = "Wildlife")]
    public class SPI_SPOW : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }




        [Column("uid")]
        public int UID { get; set; }
        [Column("dist_id")]
        public string Dist_ID { get; set; }
        [Column("cdfw_id")]
        public string CDFW_ID { get; set; }
        [Column("territory")]
        public string Territory { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("bird_status")]
        public string BirdStatus { get; set; }
        [Column("hcp_status")]
        public string HCP_Status { get; set; }
        [Column("hcp_status_2")]
        public string HCP_Status_2 { get; set; }
        [Column("occupied")]
        public string Occupied { get; set; }
        [Column("num_seen")]
        public int NumSeen { get; set; }
        [Column("adult_subadult_count")]
        public int AdultSubadultCount { get; set; }
        [Column("pair")]
        public string Pair { get; set; }
        [Column("nest")]
        public string Nest { get; set; }
        [Column("young")]
        public string Young { get; set; }
        [Column("number_of_young")]
        public int NumberOfYoung { get; set; }
        [Column("yac")]
        public string YAC { get; set; }
        [Column("response")]
        public string Response { get; set; }
        [Column("male_response")]
        public string MaleResponse { get; set; }
        [Column("female_response")]
        public string FemaleResponse { get; set; }
        [Column("unknown_sex_response")]
        public string UnknownSexResponse { get; set; }
        [Column("barred_owl_response")]
        public string BarredOwlResponse { get; set; }
        [Column("habitat_cross_plot")]
        public string HabitatCrossPlot { get; set; }
        [Column("study_area")]
        public string StudyArea { get; set; }
        [Column("unique_id")]
        public int UniqueID { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
        [Column("township")]
        public string Township { get; set; }
        [Column("range")]
        public string Range { get; set; }
        [Column("section")]
        public string Section { get; set; }
        [Column("quarter")]
        public string Quarter { get; set; }
        [Column("sixteenth")]
        public string Sixteenth { get; set; }
        [Column("source")]
        public string Source { get; set; }
        [Column("sub_species1")]
        public string SubSpecies1 { get; set; }
        [Column("age_sex")]
        public string AgeSex { get; set; }
        [Column("visit_type")]
        public string VisitType { get; set; }
        [Column("detection_type")]
        public string DetectionType { get; set; }
        [Column("observer")]
        public string Observer { get; set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("ws_id")]
        public int WS_ID { get; set; }
        [Column("utm_northing_coordinate")]
        public int UTM_NorthingCoordinate { get; set; }
        [Column("utm_easting_coordinate")]
        public int UTM_EastingCoordinate { get; set; }
        [Column("wbis_mapped_location")]
        public string WBIS_MappedLocation { get; set; }
        [Column("hex500_id")]
        public string HEX_500_ID { get; set; }









        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_SPOW>();
    }
}
