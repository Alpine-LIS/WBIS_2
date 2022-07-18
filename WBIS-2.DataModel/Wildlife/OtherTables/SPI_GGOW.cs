using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("spi_ggows")]
    [TypeGrouper(GroupName = "Wildlife")]
    public class SPI_GGOW : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }



        [Column("spi_id")]
        public string SPI_ID { get; set; }
        [Column("nest_name")]
        public string NestName { get; set; }
        [Column("territory")]
        public string Territory { get; set; }
        [Column("monitoring_group")]
        public string MonitoringGroup { get; set; }
        [Column("habitat_area")]
        public string HabitatArea{ get; set; }
        [Column("unique_id")]
        public int UniqueID { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("twn")]
        public string Twn { get; set; }
        [Column("rge")]
        public string Rge { get; set; }
        [Column("sec")]
        public string Sec { get; set; }
        [Column("quarter")]
        public string Quarter { get; set; }
        [Column("sixteenth")]
        public string Sixteenth { get; set; }
        [Column("utm_zone")]
        public string UTM_ZONE{ get; set; }
        [Column("utm_e")]
        public string UTM_E{ get; set; }
        [Column("utm_n")]
        public string UTM_N{ get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("results")]
        public string Results { get; set; }
        [Column("pair")]
        public string Pair { get; set; }
        [Column("num_nestlings")]
        public int NumNestlings { get; set; }
        [Column("num_fledglings")]
        public int NumFledglings { get; set; }
        [Column("nest")]
        public string Nest { get; set; }
        [Column("surveyors")]
        public string Surveyors { get; set; }
        [Column("year_measured")]
        public int YearMeasured { get; set; }
        [Column("year_used")]
        public int YearUsed { get; set; }
        [Column("gid_mapped")]
        public string GISMapped { get; set; }
        [Column("notes")]
        public string Notes { get; set; }










        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_GGOW>();
    }
}
