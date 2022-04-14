﻿using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class PermanentCallStation : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }



        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }




        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }

        [Column("pcs_id")]
        public string PCS_ID { get; set; }

        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new PermanentCallStationManager(); } }
    }
}
