using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class Hex160RequiredPassManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Hex160 Required Passes"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<Hex160RequiredPass> returnVal = model.Set<Hex160RequiredPass>()
                .AsNoTracking();
            var a = (Expression<Func<Hex160RequiredPass, bool>>)GetParentWhere(Query, QueryType);

                return returnVal
                .Include(_ => _.Hex160)
                .Include(_ => _.User)
                .Include(_ => _.UserModified)
                .Include(_=>_.BirdSpecies).Where(a);

        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Hex160RequiredPass, bool>> a;
            if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
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
