using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    /// <summary>
    /// An information type manager contains and describes details about how an IInformationType interacts with the WBIS application.
    /// </summary>
    public interface IInfoTypeManager : IListManager
    {
        /// <summary>
        /// A string value representing an information type.
        /// </summary>
        public string DisplayName { get; }
        /// <summary>
        /// Information type options to be displayed as children of the current information type.
        /// </summary>
        public IInformationType[] AvailibleChildren { get; }
        
    }


    /// <summary>
    /// Describes how records are displayed in a list/table
    /// </summary>
    public interface IListManager
    {
        /// <summary>
        /// Fields to be displayed when in an alternate information type.
        /// </summary>
        public List<KeyValuePair<string, string>> DisplayFields { get; }
        /// <summary>
        /// Return an IQueryable for the specified information type.
        /// </summary>
        public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model);
        /// <summary>
        /// Returns a 'Where' statment depending on a provided parent information type.
        /// </summary>
       // public abstract Expression GetParentWhere(object[] Query, Type QueryType);
        /// <summary>
        /// Can the infromation type be set as active.
        /// </summary>
        public bool CanSetActive { get; }
    }






    /// <summary>
    /// Decribes options availbe to dynamic records.
    /// Eaxamples on dynamic records are Site Callings & Botanical Surveys
    /// Eaxamples on non-dynamic records are Districts & Watersheds
    /// </summary>
    public interface IRecordOptions
    {
        /// <summary>
        /// Records can be added through an import routine.
        /// </summary>
        public bool ImportRecords { get; }

        /// <summary>
        /// Record can be added through a detail view.
        /// </summary>
        public bool AddRecord { get; }

        /// <summary>
        /// Record can be deleted.
        /// </summary>
        public bool DeleteRecord { get; }

        /// <summary>
        /// Record can be restored.
        /// </summary>
        public bool RestoreRecord { get; }

        /// <summary>
        /// If true a record can only be added when an indevidual parent is selected and the children(current information type) are presented in a detail view.
        /// </summary>
        public bool AddRequiresParent { get; }

        /// <summary>
        /// Does the record have a detail view availible.
        /// </summary>
        public bool DetailView { get; }
    }
}
