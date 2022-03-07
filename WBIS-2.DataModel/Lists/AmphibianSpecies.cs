using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class AmphibianSpecies : IInformationType
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Column("species_name")]
        public string SpeciesName{ get; set; }
        [Column("species_code")]
        public string SpeciesCode { get; set; }

        [InverseProperty("AmphibianSpecies")]
        public ICollection<AmphibianLocationFound> AmphibianLocationsFound { get; set; }
        [InverseProperty("OtherWildlife")]
        public ICollection<AmphibianPointOfInterest> AmphibianPointsOfInterest { get; set; }

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Amphibian Species"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("Species", "BirdSpecies")};
            }
        }
    }
}
