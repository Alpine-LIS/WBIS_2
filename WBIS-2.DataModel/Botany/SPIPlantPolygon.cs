using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("spi_plant_polygons")]
    [DisplayOrder(Index = 12), TypeGrouper(GroupName = "Botany"), GeometryEdits(Locked = false), ReportableTable]
    public class SPIPlantPolygon: IInformationType, INonPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Required, Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        [ListInfo(AutoInclude = true), Import]
        public PlantSpecies PlantSpecies { get; set; }
        [Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public MultiPolygon Geometry { get; set; }


        [Column("link"), Import]
        public string Link { get; set; }
        [Column("surveyor"), Import]
        public string Surveyor { get; set; }

        [Column("num_ind"), Import]
        public int NumInd { get; set; }
        [Column("num_ind_max"), Import]
        public int NumIndMax { get; set; }
        [Column("cnddb_occurrence"), Import]
        public int CNDDB_Occurrence { get; set; }

        [Column("date_time"), Import]
        public DateTime DateTime { get; set; }

        [Column("lat"), Import]
        public double Lat { get; set; }
        [Column("lon"), Import]
        public double Lon { get; set; }
        [Column("datum"), Import]
        public string Datum { get; set; }
        [Column("coord_source"), Import]
        public string CoordSource { get; set; }
        [Column("site_quality"), Import]
        public string SiteQuality { get; set; }
        [Column("disturbance"), Import]
        public string Disturbance { get; set; }
        [Column("land_use"), Import]
        public string LandUse { get; set; }
        [Column("threats"), Import]
        public string Threats { get; set; }
        [Column("hab_desc"), Import]
        public string HabDesc { get; set; }
        [Column("location"), Import]
        public string Location { get; set; }
        [Column("comments"), Import]
        public string Comments { get; set; }
        [Column("land_owner"), Import]
        public string Landowner { get; set; }
        [Column("obs_contract"), Import]
        public string ObsContact { get; set; }
        [Column("associated"), Import]
        public string Associated { get; set; }
        [Column("name1_"), Import]
        public string NAME1_ { get; set; }

        [Column("vegetative"), Import]
        public int Vegetative { get; set; }
        [Column("flowering"), Import]
        public int Flowering { get; set; }
        [Column("fruiting"), Import]
        public int Fruiting { get; set; }



        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
       public ICollection<Watershed> Watersheds { get; set;}
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }

        public bool _delete { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPIPlantPolygon>();
    }
}
