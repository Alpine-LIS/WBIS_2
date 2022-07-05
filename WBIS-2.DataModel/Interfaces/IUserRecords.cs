using Alpine.EFTools.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IUserRecords : IInformationType, IDeleteRepository
    {
        /// <summary>
        /// The user who created the record
        /// </summary>
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid? UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        
        /// <summary>
        /// Repository data may be toggled on or off. 
        /// Acts as a way to store historic data and keep it out of the way.
        /// </summary>
        public bool Repository { get; set; }
        public Guid Id { get; set; }
    }
}
