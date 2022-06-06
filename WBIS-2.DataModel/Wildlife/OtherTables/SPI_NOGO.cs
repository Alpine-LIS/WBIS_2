using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class SPI_NOGO : IInformationType, IWildlifeRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


        [Column("dist_id")]
        public string Dist_ID {get; set;}
        [Column("nest_id")]
        public int NestID { get; set; }
        [Column("territory")]
        public string Territory { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("nest_name")]
        public string NestName { get; set; }
        [Column("territory_status")]
        public string TerritoryStatus{ get; set; }
        [Column("pair")]
        public string Pair { get; set; }
        [Column("nest")]
        public string Nest { get; set; }
        [Column("young")]
        public string Young { get; set; }
        [Column("number_of_young")]
        public int NumberOfYoung { get; set; }
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
        [Column("comments")]
        public string Comments { get; set; }
        [Column("surveyor")]
        public string Surveyor { get; set; }
        [Column("owner")]
        public string Owner { get; set; }
        [Column("unique_id")]
        public int UNIQUEID { get; set; }
        [Column("_300m_search")]
        public string _300MSearch{ get; set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("transmitter")]
        public string Transmitter { get; set; }
        [Column("ref_loc")]
        public string RefLoc{ get; set; }
        [Column("usfs_exchange")]
        public string USFS_Exchange{ get; set; }
        [Column("wsid")]
        public int WSID { get; set; }
        [Column("utm_northing_coordinate")]
        public int UTM_NorthingCoordinate { get; set; }
        [Column("utm_easting_coordinate")]
        public int UTM_EastingCoordinate { get; set; }




[Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_NOGO>();
    }
}
