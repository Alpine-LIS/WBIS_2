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
    [Table("other_wildlife_records")]
    [DisplayOrder(Index = 16), TypeGrouper(GroupName = "Wildlife")]
    public class OtherWildlife : UserDataValidator, IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        public bool _delete { get; set; }
       
      


        [Required, Column("wildlife_species_id")]
        public Guid WildlifeSpeciesId { get; set; }
        [ListInfo(AutoInclude = true)]
        public WildlifeSpecies WildlifeSpecies { get; set; }

        [Column("detection"),]
        public string Detection { get; set; }
        [Column("number"),]
        public int Number { get; set; }
        [Column("date_time"),]
        public DateTime DateTime { get; set; }

        [Column("site_calling_id"), ]
        public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new InformationTypeManager<OtherWildlife>(); } }
    }
}
