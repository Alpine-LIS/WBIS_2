using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BirdSpecies : IInformationType
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Column("species")]
        public string Species { get; set; }
        [InverseProperty("SurveySpecies")]
        public ICollection<SiteCalling> SurveySpecies { get; set; }
        [InverseProperty("SpeciesFound")]
        public ICollection<SiteCalling> SpeciesFound { get; set; }
        public ICollection<Hex160RequiredPass> PassSpecies { get; set; }

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Bird Species"; } }

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
