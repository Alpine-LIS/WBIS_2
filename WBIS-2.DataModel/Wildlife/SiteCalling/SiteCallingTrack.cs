﻿using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    [SubElement(typeof(SiteCalling))]
    public class SiteCallingTrack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid"), ForeignKey("SiteCalling")]
        public Guid Guid { get; set; }

        //[Required, Column("site_calling_id"), ForeignKey("SiteCalling")]
        //public Guid SiteCallingId { get; set; }
        public SiteCalling SiteCalling { get; set; }

        [Column("geometry", TypeName = "geometry(LineString,26710)")]
        public LineString Geometry { get; set; }


    }
}
