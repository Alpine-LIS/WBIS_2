using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
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
        [NotMapped]
        public IInformationType[] AvailibleChildren { get; }
       
        public abstract Expression<Func<object, bool>> GetParentWhere(object[] Query, Type QueryType);
    }
}
