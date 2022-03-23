using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class Region
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }


        [Column("region_name")]
        public string RegionName { get; set; }

        public ICollection<PlantProtectionSummary> PlantProtectionSummaries { get; set; }
        public ICollection<RegionalPlantList> RegionalPlantLists { get; set; }
        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
    }
}
