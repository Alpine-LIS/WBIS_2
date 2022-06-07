using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBIS_2.DataModel
{
    [Keyless, Table("scoping_texts")]
    public class ScopingText
    {
        [Column("field")]
        public string Field { get; set; }
        [Column("header")]
        public string Header { get; set; }
        [Column("text")]
        public string Text { get; set; }
    }
}
