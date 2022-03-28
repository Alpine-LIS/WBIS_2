using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Keyless]
    public class ReproductiveStatus 
    {
        [Column("status")]
        public string Status { get; set; }
    }
}
