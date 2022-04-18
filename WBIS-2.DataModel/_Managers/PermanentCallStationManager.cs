using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class PermanentCallStationManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Permanent Call Stations"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        [NotMapped]
        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<PermanentCallStation> returnVal = model.Set<PermanentCallStation>()
                .AsNoTracking();
            var a = (Expression<Func<PermanentCallStation, bool>>)GetParentWhere(Query, QueryType);

                return returnVal
                    .Include(_ => _.Hex160)
                .Include(_ => _.User)
                .Include(_ => _.UserModified).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<PermanentCallStation, bool>> a;
            if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            a = _ => Query.Contains(_);
            return a;
        }

        public bool ImportRecords => true;

        public bool AddRecord => false;

        public bool AddRequiresParent => false;

        public bool DetailView => false;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;// => false;
    }
}
