using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IUserRecords : IInformationType
    {
        /// <summary>
        /// The user who created the record
        /// </summary>
        public ApplicationUser User { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        public Guid Guid { get; set; }
    }
}
