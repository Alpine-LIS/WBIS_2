using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("watersheds")]
    [DisplayOrder(Index = 2), TypeGrouper(IgnoreGroups = true)]
    public class Watershed : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required, Column("watershed_name"), ListInfo(DisplayField = true)]
        public string WatershedName { get; set; }
        [Required, Column("watershed_id"), ListInfo(DisplayField = true), Import]
        public string WatershedID { get; set; }

        [Column("mouth_trs")]
        public string MouthTRS { get; set; }
        [Column("mouth_lat")]
        public double MouthLat { get; set; }
        [Column("mouth_lon")]
        public double MouthLon { get; set; }
        [Column("elevation_min")]
        public double ElevationMin { get; set; }
        [Column("elevation_max")]
        public double ElevationMax { get; set; }
        [Column("basin_length")]
        public double BasinLength { get; set; }
        [Column("perim001")]
        public double Perim001 { get; set; }
        [Column("vally_length")]
        public double VallyLength { get; set; }
        [Column("chanel_length")]
        public double ChanelLength { get; set; }
        [Column("chanel_orientation")]
        public string ChanelOrientation { get; set; }
        [Column("ws_order")]
        public double WS_Order { get; set; }
        [Column("humint_pop")]
        public string HumintPop { get; set; }
        [Column("humint_rec")]
        public string HumintRec { get; set; }
        [Column("humint_vis")]
        public string HumintVis { get; set; }
        [Column("geology")]
        public string Geology { get; set; }
        [Column("basin_m_type")]
        public int BasinMType { get; set; }
        [Column("river_name")]
        public string RiverName { get; set; }
        [Column("down_str_wshd")]
        public string DownStrWshd { get; set; }
        [Column("hydro_reg")]
        public string HydroReg { get; set; }
        [Column("rwqcb")]
        public string RWQCB { get; set; }
        [Column("hydrologic")]
        public string Hydrologic { get; set; }
        [Column("hydro_area")]
        public string HydroArea { get; set; }
        [Column("hydro_suba")]
        public string HydroSuba { get; set; }
        [Column("super_plan")]
        public string SuperPlan { get; set; }
        [Column("threatend")]
        public string Threatend { get; set; }
        [Column("asp_up")]
        public string ASP_UP { get; set; }
        [Column("toc")]
        public bool? TOC { get; set; }
        [Column("esu")]
        public bool? ESU { get; set; }
        [Column("d303")]
        public string D303 { get; set; }
        [Column("wshd_acres")]
        public double WshdAcres { get; set; }
        [Column("spi_acres")]
        public double SPIAcres { get; set; }
        [Column("perimeter")]
        public double Perimeter { get; set; }
             

        [Column("geometry", TypeName ="geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }


        public ICollection<District> Districts { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<Hex160> Hex160s { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<Quad75> Quad75s { get; set; }

        [ListInfo(ChildField =  true)]
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }

        [ListInfo(ChildField =  true)]
        public ICollection<SiteCalling> SiteCallings { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<SiteCallingDetection> SiteCallingDetections { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<OwlBanding> OwlBandings { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get;set;}
        [ListInfo(ChildField =  true)]
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<AmphibianElement> AmphibianElements { get; set; }

        [ListInfo(ChildField =  true)]
        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        [ListInfo(ChildField =  true)]
        public ICollection<BotanicalElement> BotanicalElements { get; set; }


        //[ListInfo(ChildField = true)]
        //public ICollection<ForestCarnivoreCameraStation> ForestCarnivoreCameraStations { get; set; }
        //[ListInfo(ChildField = true)]
        //public ICollection<RanchPhotoPoint> RanchPhotoPoints { get; set; }

        //[ListInfo(ChildField = true)]
        //public ICollection<DOMonitoring> DOMonitorings { get; set; }


        //[ListInfo(ChildField = true)]
        //public ICollection<BDOWSighting> BDOWSightings { get; set; }


        [ListInfo(ChildField = true)]
        public ICollection<SPI_GGOW> SPI_GGOWs { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPI_SPOW> SPI_SPOWs { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPI_NOGO> SPI_NOGOs { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<SPI_WildlifeSighting> SPI_WildlifeSightings { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<Watershed>();
    }
}
