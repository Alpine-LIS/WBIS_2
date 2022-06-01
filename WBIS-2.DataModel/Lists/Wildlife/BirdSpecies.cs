using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BirdSpecies : UserDataValidator, IInformationType, IPlaceHolder
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Column("species"), ImportAttribute(Required = true), ListInfo (DisplayField = true)]
        public string Species { get; set; }

        [Column("is_surveyable")]
        public bool IsSurveyable { get; set; }
        [Column("is_findable")]
        public bool IsFindable { get; set; }
        [Column("banding_species")]
        public bool BandingSpecies { get; set; }
        [Column("place_holder")]
        public bool PlaceHolder { get; set; }


        [InverseProperty("SurveySpecies")]
        public ICollection<SiteCalling> SurveySpecies { get; set; }
        [InverseProperty("SpeciesFound")]
        public ICollection<SiteCallingDetection> SpeciesFound { get; set; }


        public ICollection<OwlBanding> OwlBandings { get; set; }
        public ICollection<Hex160RequiredPass> PassSpecies { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<BirdSpecies>();
    }
}
