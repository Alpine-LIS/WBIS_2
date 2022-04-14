using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class BotanicvalScopingManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Botanical Scoping"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalSurveyArea(), new BotanicalSurvey(), new BotanicalElement() }; }
        }



        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<BotanicalScoping> returnVal = model.Set<BotanicalScoping>()
                .AsNoTracking()
                .Include(_ => _.Region)
                .Include(_ => _.THP_Area)
                .Include(_ => _.User)
                .Include(_ => _.UserModified);
            var a = (Expression<Func<BotanicalScoping, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                returnVal = returnVal.Include(_ => _.Districts).Where(a);
            else if (QueryType == typeof(Watershed))
                returnVal = returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                returnVal = returnVal.Include(_ => _.Quad75s).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }

        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<BotanicalScoping, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
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

        public bool ImportRecords => false;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public string AddToolTip => "Add Record";

        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;
    }
}
