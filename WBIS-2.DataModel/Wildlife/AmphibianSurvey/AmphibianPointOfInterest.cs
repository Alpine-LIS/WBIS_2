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
    public class AmphibianPointOfInterest : UserDataValidator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid"), ForeignKey("AmphibianElement")]
        public Guid Guid { get; set; }

        //[Required, Column("amphibian_element_id"),ForeignKey("AmphibianElement")]
        //public Guid AmphibianElementId { get; set; }
        public AmphibianElement AmphibianElement { get; set; }


        [Required,Column("point_of_interest")]
        public string PointOfInterest { get; set; }

        [Column("other_wildlife_id")]
        public Guid OtherWildlifeId { get; set; }
        public AmphibianSpecies OtherWildlife { get; set; }
    }
}
