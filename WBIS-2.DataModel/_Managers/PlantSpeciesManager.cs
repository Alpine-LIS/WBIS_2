using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class PlantSpeciesManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Plant Species"; } }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<PlantSpecies> returnVal = model.Set<PlantSpecies>()
                .AsNoTracking();
            var a = (Expression<Func<PlantSpecies, bool>>)GetParentWhere(Query, QueryType);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<PlantSpecies, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("SciName", "PlantSpecies"),
                new KeyValuePair<string, string>("ComName", "PlantSpecies")};
            }
        }

        public bool ImportRecords => false;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public string AddToolTip => "Add Record";

        public bool DetailView => true;
        public bool DeleteRecord => false;
        public bool RestoreRecord => false;
        public bool CanSetActive => false;// => false;
    }
}
