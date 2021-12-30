using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BirdSpecies
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
    }
}
