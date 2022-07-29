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



        [Column("spi_id"), Import]
        public string SPI_ID { get; set; }
        [Column("nest_name"), Import]
        public string NestName { get; set; }
        [Column("territory"), Import]
        public string Territory { get; set; }
        [Column("monitoring_group"), Import]
        public string MonitoringGroup { get; set; }
        [Column("habitat_area"), Import]
        public string HabitatArea{ get; set; }
        [Column("unique_id"), Import]
        public int UniqueID { get; set; }
        [Column("year"), Import]
        public int Year { get; set; }
        [Column("twn"), Import]
        public string Twn { get; set; }
        [Column("rge"), Import]
        public string Rge { get; set; }
        [Column("sec"), Import]
        public string Sec { get; set; }
        [Column("quarter"), Import]
        public string Quarter { get; set; }
        [Column("sixteenth"), Import]
        public string Sixteenth { get; set; }
        [Column("utm_zone"), Import]
        public string UTM_ZONE{ get; set; }
        [Column("utm_e"), Import]
        public string UTM_E{ get; set; }
        [Column("utm_n"), Import]
        public string UTM_N{ get; set; }
        [Column("longitude"), Import]
        public double Longitude { get; set; }
        [Column("latitude"), Import]
        public double Latitude { get; set; }
        [Column("results"), Import]
        public string Results { get; set; }
        [Column("pair"), Import]
        public string Pair { get; set; }
        [Column("num_nestlings"), Import]
        public int NumNestlings { get; set; }
        [Column("num_fledglings"), Import]
        public int NumFledglings { get; set; }
        [Column("nest"), Import]
        public string Nest { get; set; }
        [Column("surveyors"), Import]
        public string Surveyors { get; set; }
        [Column("year_measured"), Import]
        public int YearMeasured { get; set; }
        [Column("year_used"), Import]
        public int YearUsed { get; set; }
        [Column("gid_mapped"), Import]
        public string GISMapped { get; set; }
        [Column("notes"), Import]
        public string Notes { get; set; }










        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true), Import]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        [Import]
        public Watershed Watershed { get; set; }

        public bool _delete { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_GGOW>();
    }
}
