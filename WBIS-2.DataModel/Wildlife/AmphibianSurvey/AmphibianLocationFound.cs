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
    public class AmphibianLocationFound : UserDataValidator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid"), ForeignKey("AmphibianElement")]
        public Guid Guid { get; set; }

        //[Required, Column("amphibian_element_id"), ForeignKey("AmphibianElement")]
        //public Guid AmphibianElementId { get; set; }
        public AmphibianElement AmphibianElement { get; set; }


        [Required, Column("point_of_interest")]
        public string PointOfInterest { get; set; }

        [Required, Column("amphibian_species_id")]
        public Guid AmphibianSpeciesId { get; set; }
        public AmphibianSpecies AmphibianSpecies { get; set; }


        [Column("number_of_adults")]
        public double NumberOfAdults { get; set; }
        [Column("number_of_subadults")]
        public double NumberOfSubadults { get; set; }
        [Column("number_of_larve")]
        public double NumberOfLarve { get; set; }
        [Column("number_of_egg_masses")]
        public double NumberOfEggMasses { get; set; }

        [Column("visual")]
        public bool Visual { get; set; }
        [Column("aural")]
        public bool Aural { get; set; }
    }
}
