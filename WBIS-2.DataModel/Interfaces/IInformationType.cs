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
        public IInformationType[] AvailibleChildren { get; }
    }
    public interface IQueryStuff<T> where T : class
    {
        public abstract Expression<Func<T, bool>> GetParentWhere(object[] Query, Type QueryType);
    }
}
