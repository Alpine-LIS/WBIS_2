using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class Hex160 : IInformationType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("hex160_id")]
        public string Hex160ID { get; set; }
        [Column("calling_responses")]
        public int CallingResponses { get; set; }
        [Column("follow_ups")]
        public int FollowUps { get; set; }
        [Column("skips")]
        public int Skips { get; set; }
        [Column("drops")]
        public int Drops { get; set; }
        [Column("recent_activity")]
        public string RecentActivity { get; set; }
        [Column("latest_activity")]
        public DateTime LatestActivity { get; set; }



        [Column("geometry"), DataType("geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }


        
        public ICollection<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        public ICollection<SiteCalling> SiteCallings { get; set; }


        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }

       

        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Hex160"; } }
    }
}
