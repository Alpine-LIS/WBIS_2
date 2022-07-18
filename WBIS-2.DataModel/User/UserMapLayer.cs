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
    [Table("user_map_layers")]
    public class UserMapLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Column("application_user_id"), ForeignKey("ApplicationUser")]
        public Guid? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Column("information_type")]
        public string InformationType { get; set; }
        [Column("visible_layer")]
        public string VisibleLayer { get; set; }
    }
}
