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
    [Keyless, Table("cdfw_vintages")]
    public class CdfwVintage
    {
        public DateTime date { get; set; } = DateTime.Now;
    }
}
