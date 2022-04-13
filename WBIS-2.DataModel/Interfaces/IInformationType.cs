using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        /// <summary>
        /// Fields to be displayed when in an alternate information type.
        /// </summary>
        public static List<KeyValuePair<string, string>> DisplayFields { get; }
    }
    //public interface IQueryStuff<T> where T : class
    //{
    //    public abstract Expression<Func<T, bool>> GetParentWhere(object[] Query, Type QueryType);
    //}
    public interface IQueryStuff
    {
        public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model);
        public abstract Expression GetParentWhere(object[] Query, Type QueryType);
    }
}
