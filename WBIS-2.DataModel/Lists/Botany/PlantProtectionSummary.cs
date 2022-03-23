using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class PlantProtectionSummary
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }

        [Required, Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }

        [Column("summary")]
        public string Summary { get; set; }

        [Column("region_id")]
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}
