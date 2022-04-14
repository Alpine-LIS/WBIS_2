using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class SPIPlantPolygon: IInformationType, INonPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }
        [Required, Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public MultiPolygon Geometry { get; set; }


        [Column("link")]
        public string Link { get; set; }
        [Column("surveyor")]
        public string Surveyor { get; set; }

        [Column("num_ind")]
        public int NumInd { get; set; }
        [Column("num_ind_max")]
        public int NumIndMax { get; set; }
        [Column("cnddb_occurrence")]
        public int CNDDB_Occurrence { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; }

        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }
        [Column("coord_source")]
        public string CoordSource { get; set; }
        [Column("site_quality")]
        public string SiteQuality { get; set; }
        [Column("disturbance")]
        public string Disturbance { get; set; }
        [Column("land_use")]
        public string LandUse { get; set; }
        [Column("threats")]
        public string Threats { get; set; }
        [Column("hab_desc")]
        public string HabDesc { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
        [Column("land_owner")]
        public string Landowner { get; set; }
        [Column("obs_contract")]
        public string ObsContact { get; set; }
        [Column("associated")]
        public string Associated { get; set; }
        [Column("name1_")]
        public string NAME1_ { get; set; }

        [Column("vegetative")]
        public int Vegetative { get; set; }
        [Column("flowering")]
        public int Flowering { get; set; }
        [Column("fruiting")]
        public int Fruiting { get; set; }



        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
       public ICollection<Watershed> Watersheds { get; set;}
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new SPIPlantPolygonManager(); } }
    }
}
