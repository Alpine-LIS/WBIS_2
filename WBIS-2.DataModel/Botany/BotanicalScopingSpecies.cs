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
    public class BotanicalScopingSpecies : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }

        [Column("botanical_scoping_id")]
        public Guid BotanicalScopingId { get; set; }
        public BotanicalScoping BotanicalScoping { get; set; }

        [Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }

        [Column("exclude")]
        public bool Exclude { get; set; } = false;
        [Column("exclude_text")]
        public string ExcludeText { get; set; }
        [Column("exclude_report")]
        public bool ExcludeReport { get; set; } = false;


        [Column("habitat_description")]
        public string HabitatDescription { get; set; }
        [Column("nddb_habitat_description")]
        public string NddbHabitatDescription { get; set; }
        [Column("spi_habitat_description")]
        public string SpiHabitatDescription { get; set; }


        [Column("protection_summary")]
        public string ProtectionSummary { get; set; }


        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        //[Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }


        [Column("user_id")]
        public Guid? UserId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }

        public IInfoTypeManager Manager => new InformationTypeManager<BotanicalScopingSpecies>();
    }
}
