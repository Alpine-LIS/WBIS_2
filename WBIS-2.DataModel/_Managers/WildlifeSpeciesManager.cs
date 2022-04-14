using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class WildlifeSpeciesManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Wildlife Species"; } }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<WildlifeSpecies> returnVal = model.Set<WildlifeSpecies>()
                .AsNoTracking();
            var a = (Expression<Func<WildlifeSpecies, bool>>)GetParentWhere(Query, QueryType);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<WildlifeSpecies, bool>> a = _ => Query.Contains(_);
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
                { new KeyValuePair<string, string>("AlphaCode", "WildlifeSpecies"),
                new KeyValuePair<string, string>("Species", "WildlifeSpecies")};
            }
        }

        public bool ImportRecords => false;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public string AddToolTip => "Add Record";

        public bool DetailView => true;
        public bool DeleteRecord => false;
        public bool RestoreRecord => false;
        public bool SetAvtive => false;
        public bool CanSetActive => false;
    }
}
