using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("flowering_timelines")]
    public class FloweringTimeline
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Required, Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }

        [Column("active_from")]
       public string ActiveFrom { get; set; }
        [Column("active_to")]
        public string ActiveTo { get; set; }
    }
}
