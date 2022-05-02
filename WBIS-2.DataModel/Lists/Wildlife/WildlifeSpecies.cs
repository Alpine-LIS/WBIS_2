using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class WildlifeSpecies : IInformationType, IPlaceHolder
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Column("alpha_code"), ImportAttribute(Required = true), ListInfo(DisplayField = true)]
        public string AlphaCode { get; set; }
        [Column("wildlife_species_description")]
        public string WildlifeSpeciesDescription { get; set; }
        [Column("class")]
        public string Class { get; set; }
        [Column("order")]
        public string Order { get; set; }
        [Column("family")]
        public string Family { get; set; }
        [Column("genus")]
        public string Genus { get; set; }
        [Column("species"), ListInfo(DisplayField = true)]
        public string Species { get; set; }
        [Column("wl_sorting")]
        public int WLSorting { get; set; }
        [Column("sub_species")]
        public string SubSpecies { get; set; }
        [Column("whr_num")]
        public string WHRNum { get; set; }
        [Column("place_holder")]
        public bool PlaceHolder { get; set; }

        public ICollection<OtherWildlife> OtherWildlifeRecords { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<WildlifeSpecies>();
    }
}
