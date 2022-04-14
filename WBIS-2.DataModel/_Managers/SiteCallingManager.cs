using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class SiteCallingManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Site Calling"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new SiteCallingDetection() }; }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<SiteCalling> returnVal = model.Set<SiteCalling>()
                .AsNoTracking()
                .Include(_ => _.User)
                .Include(_ => _.UserModified)
                .Include(_ => _.SurveySpecies)
                .Include(_ => _.ProtectionZone)
                .Include(_ => _.District)
                .Include(_ => _.Hex160);
            var a = (Expression<Func<SiteCalling, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(Watershed))
                returnVal = returnVal.Include(_ => _.Watershed).Where(a);
            else if (QueryType == typeof(Quad75))
                returnVal = returnVal.Include(_ => _.Quad75).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<SiteCalling, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Hex160.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Hex160.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Hex160.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
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
