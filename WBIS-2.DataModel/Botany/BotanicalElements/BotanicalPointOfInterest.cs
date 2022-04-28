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
    public class BotanicalPointOfInterest : UserDataValidator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid"), ForeignKey("BotanicalElement")]
        public Guid Guid { get; set; }

        //[Required, Column("botanical_element_id"), ForeignKey("BotanicalElement")]
        //public Guid BotanicalElementId { get; set; }
        public BotanicalElement BotanicalElement { get; set; }


        [Column("record_type")]
        public string RecordType { get; set; }
        [Column("inundated")]
        public bool Inundated { get; set; } = false;
        [Column("littoral_zone")]
        public bool LittoralZone { get; set; } = false;
        [Column("herbaceous_vegetation")]
        public bool HerbaceousVegetation { get; set; } = false;
        [Column("woody_vedetation")]
        public bool WoodyVegetation { get; set; } = false;
        [Column("instream")]
        public bool Instream { get; set; } = false;
        [Column("isolated")]
        public bool Isolated { get; set; } = false;
        [Column("rechecks_needed")]
        public bool RechecksNeeded { get; set; } = false;
        [Column("recheck")]
        public bool Recheck { get; set; } = false;


        [Column("substrate")]
        public string Substrate { get; set; }
        [Column("gradient")]
        public string Gradient { get; set; }
        [Column("radius")]
        public double Radius { get; set; }

        [Required, Column("date_time")]
        public DateTime DateTime { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
    }
}
