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

        [Column("district_id")]
        public Guid DistrictId { get; set; }
        public District District { get; set; }
    }
}
