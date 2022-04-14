using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class AmphibianSurveyManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Amphibian Survey"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new AmphibianElement() }; }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<AmphibianSurvey> returnVal = model.Set<AmphibianSurvey>()
                .AsNoTracking();
            var a = (Expression<Func<AmphibianSurvey, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                returnVal = returnVal.Include(_ => _.District).Where(a);
            else if (QueryType == typeof(Watershed))
                returnVal = returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                returnVal = returnVal.Include(_ => _.Quad75s).Where(a);
            else if (QueryType == typeof(Hex160))
                returnVal = returnVal.Include(_ => _.Hex160s).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<AmphibianSurvey, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        public bool ImportRecords => true;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;
    }
}
