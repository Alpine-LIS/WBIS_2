using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IInformationType
    {
        /// <summary>
        /// The user who created the record
        /// </summary>
        [NotMapped]
        public string DisplayName { get; }
    }
}
