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
    public class OtherWildlife : UserDataValidator//, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        public bool _delete { get; set; }
       
      


        [Required, Column("wildlife_species_id")]
        public Guid WildlifeSpeciesId { get; set; }
        public WildlifeSpecies WildlifeSpecies { get; set; }
        
        [Column("site_calling_id"), ]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

      

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Other Wildlife"; } }

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
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
