using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("spi_nogos")]
    [TypeGrouper(GroupName = "Wildlife")]
    public class SPI_NOGO : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }


        [Column("dist_id"), Import]
        public string Dist_ID {get; set;}
        [Column("nest_id"), Import]
        public int NestID { get; set; }
        [Column("territory"), Import]
        public string Territory { get; set; }
        [Column("year"), Import]
        public int Year { get; set; }
        [Column("nest_name"), Import]
        public string NestName { get; set; }
        [Column("territory_status"), Import]
        public string TerritoryStatus{ get; set; }
        [Column("pair"), Import]
        public string Pair { get; set; }
        [Column("nest"), Import]
        public string Nest { get; set; }
        [Column("young"), Import]
        public string Young { get; set; }
        [Column("number_of_young"), Import]
        public int NumberOfYoung { get; set; }
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
        [Column("comments"), Import]
        public string Comments { get; set; }
        [Column("surveyor"), Import]
        public string Surveyor { get; set; }
        [Column("owner"), Import]
        public string Owner { get; set; }
        [Column("unique_id"), Import]
        public int UNIQUEID { get; set; }
        [Column("_300m_search"), Import]
        public string _300MSearch{ get; set; }
        [Column("notes"), Import]
        public string Notes { get; set; }
        [Column("transmitter"), Import]
        public string Transmitter { get; set; }
        [Column("ref_loc"), Import]
        public string RefLoc{ get; set; }
        [Column("usfs_exchange"), Import]
        public string USFS_Exchange{ get; set; }
        [Column("wsid"), Import]
        public int WSID { get; set; }
        [Column("utm_northing_coordinate"), Import]
        public int UTM_NorthingCoordinate { get; set; }
        [Column("utm_easting_coordinate"), Import]
        public int UTM_EastingCoordinate { get; set; }




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
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_NOGO>();
    }
}
