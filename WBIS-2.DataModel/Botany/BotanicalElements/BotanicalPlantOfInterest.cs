using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalPlantOfInterest : UserDataValidator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("botanical_element_id"), ForeignKey("BotanicalElement")]
        public Guid BotanicalElementId { get; set; }
        public BotanicalElement BotanicalElement { get; set; }

        [Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }


        [Column("species_found")]
        public bool SpeciesFound { get; set; } = false;
        [Column("species_found_text")]
        public string SpeciesFoundText { get; set; }


        [Column("num_ind")]
        public int NumInd { get; set; }
        [Column("num_ind_max")]
        public int NumIndMax { get; set; }
        [Column("subsequent_visit")]
        public bool SubsequentVisit { get; set; }
        [Column("existing_cnddb")]
        public bool ExistingCNDDB { get; set; }

        [Column("occ_num")]
        public int OccNum { get; set; }
        [Column("vegetative")]
        public int Vegetative { get; set; }
        [Column("flowering")]
        public int Flowering { get; set; }
        [Column("fruiting")]
        public int Fruiting { get; set; }


        [Required, Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }



        [Column("radius")]
        public double Radius { get; set; }
        [Column("site_quality")]
        public string SiteQuality { get; set; }
        [Column("habitat")]
        public string Habitat { get; set; }
        [Column("land_use")]
        public string LandUse { get; set; }
        [Column("disturbances")]
        public string Disturbances { get; set; }
        [Column("threats")]
        public string Threats { get; set; }
       
    }
}
