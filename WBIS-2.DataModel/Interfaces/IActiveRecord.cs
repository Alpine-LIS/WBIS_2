using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IActiveUnit 
    {
        public bool IsActive { get; }
        public ICollection<ApplicationUser> ActiveUsers { get; set; }
    }
}
