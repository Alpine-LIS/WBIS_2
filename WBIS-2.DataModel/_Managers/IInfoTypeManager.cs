using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WBIS_2.DataModel
{
    /// <summary>
    /// An information type manager contains and describes details about how an IInformationType interacts with the WBIS application.
    /// </summary>
    public interface IInfoTypeManager
    {
        /// <summary>
        /// A string value representing an information type.
        /// </summary>
        public string DisplayName { get; }
        public string TableName { get; }
        /// <summary>
        /// Information type options to be displayed as children of the current information type.
        /// </summary>
        public Type InformationType { get; }
        public IInformationType[] AvailibleChildren { get; }
        public IInformationType[] PossibleParents { get; }

        /// Fields to be displayed when in an alternate information type.
        /// </summary>
        public List<KeyValuePair<string, string>> DisplayFields { get; }
        public IEnumerable<PropertyInfo> DisplayFieldProperties { get; }
        /// <summary>
        /// Return an IQueryable for the specified information type.
        /// </summary>
        public abstract IQueryable GetQueryable(WBIS2Model model, bool track = false, List<string> ForceInclude = null, bool includeGeometry = false);
       // public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model);
        public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool showDelete = false, bool showRepository = false, 
            bool includeGeometry = false, bool track = false, List<string> ForceInclude = null);
        //public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool showDelete, bool showRepository, bool includeGeometry);
        //public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool showDelete, bool showRepository);
        //public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool track = false);
        //public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, List<string> ForceInclude = null);
        public abstract IQueryable GetQueryableFromChildren(object[] Query, Type QueryType, WBIS2Model model);
        /// <summary>
        /// Can the infromation type be set as active.
        /// </summary>
        public bool CanSetActive { get; }

        /// <summary>
        /// Records can be added through an import routine.
        /// </summary>
        public bool ImportRecords { get; }

        /// <summary>
        /// Record can be added through a detail view.
        /// </summary>
        //public bool AddRecord { get; }

        /// <summary>
        /// Record can be deleted.
        /// </summary>
        public bool DeleteRestoreRecord { get; }
        public bool RepositoryRecord { get; }

       

        /// <summary>
        /// If true a record can only be added when an indevidual parent is selected and the children(current information type) are presented in a detail view.
        /// </summary>
        public bool AddRequiresParent { get; }

        /// <summary>
        /// Does the record have a detail view availible.
        /// </summary>
        //public bool DetailView { get; }

        public SubstituteLayer SubstituteLayer { get; }
        public abstract string GetLayerName();
        public string GetSqlQuery(List<string> exlude);
    }
}
