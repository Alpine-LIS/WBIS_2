using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class ProtectionZoneManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Proection Zone"; } }

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
            IQueryable<ProtectionZone> returnVal = model.Set<ProtectionZone>()
                .AsNoTracking();
            var a = (Expression<Func<ProtectionZone, bool>>)GetParentWhere(Query, QueryType);

            return returnVal
                .Include(_ => _.Hex160s)
                .Include(_=>_.User)
                .Include(_=>_.UserModified).Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<ProtectionZone, bool>> a;
            if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            a = _ => Query.Contains(_);
            return a;
        }

        public bool ImportRecords => true;

        public bool AddRecord => false;

        public bool AddRequiresParent => false;

        public bool DetailView => false;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;
    }
}
