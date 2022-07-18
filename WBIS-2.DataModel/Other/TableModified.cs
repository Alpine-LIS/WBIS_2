using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.DataModel
{
    [Table("tables_modified")]
    [Keyless]
    public class TableModified
    {
        [Column("table_name")]
       public string TableName { get;set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
    }
}
