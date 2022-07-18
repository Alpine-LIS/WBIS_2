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
    [Table("amphibian_locations_found")]
    public class AmphibianLocationFound : UserDataValidator, IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id"), ForeignKey("AmphibianElement")]
        public Guid Id { get; set; }

        //[Required, Column("amphibian_element_id"), ForeignKey("AmphibianElement")]
        //public Guid AmphibianElementId { get; set; }
        public AmphibianElement AmphibianElement { get; set; }


       

        [Column("amphibian_species_id"), ListInfo(AutoInclude = true)]
        public Guid? AmphibianSpeciesId { get; set; }
        public AmphibianSpecies AmphibianSpecies { get; set; }


        [Column("number_of_adults"), ListInfo(DisplayField = true)]
        public double NumberOfAdults { get; set; }
        [Column("number_of_subadults"), ListInfo(DisplayField = true)]
        public double NumberOfSubadults { get; set; }
        [Column("number_of_larve"), ListInfo(DisplayField = true)]
        public double NumberOfLarve { get; set; }
        [Column("number_of_egg_masses"), ListInfo(DisplayField = true)]
        public double NumberOfEggMasses { get; set; }

        [Column("visual"), ListInfo(DisplayField = true)]
        public bool Visual { get; set; }
        [Column("aural"), ListInfo(DisplayField = true)]
        public bool Aural { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<AmphibianLocationFound>();
    }
}
