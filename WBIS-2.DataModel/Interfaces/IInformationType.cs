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
        public IInfoTypeManager<IInformationType> Manager { get; }
    }
    //public interface IQueryStuff
    //{
    //    public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model);
    //    public abstract Expression GetParentWhere(object[] Query, Type QueryType);
    //}
    /// <summary>
    /// An information type manager contains and describes details about how an IInformationType interacts with the WBIS application.
    /// </summary>
    public interface IInfoTypeManager<T> where T : class
    {
        public string DisplayName { get; }
        public IInformationType[] AvailibleChildren { get; }
        /// <summary>
        /// Fields to be displayed when in an alternate information type.
        /// </summary>
        public List<KeyValuePair<string, string>> DisplayFields { get; }
        /// <summary>
        /// Return an IQueryable for the specified information type.
        /// </summary>
        public abstract IQueryable<T> GetQueryable(object[] Query, Type QueryType, WBIS2Model model);
        /// <summary>
        /// Returns a 'Where' statment depending on a provided parent information type.
        /// </summary>
        public abstract Expression<Func<T, bool>> GetParentWhere(object[] Query, Type QueryType);
    }
}
