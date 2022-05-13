using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Keyless]
    public class DropdownOption
    {
        [Column("entity")]
        public string Entity { get; set; }
        [Column("property")]
        public string Property { get; set; }
        [Column("full_text")]
        public string FullText { get; set; }
        [Column("selection_text")]
        public string SelectionText { get; set; }
    }
}
