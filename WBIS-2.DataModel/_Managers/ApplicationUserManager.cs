using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class ApplicationUserManager : IInfoTypeManager, IRecordOptions
    {
        
        public string DisplayName { get { return "Application User"; } }

        [NotMapped]
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
                { new KeyValuePair<string, string>("UserName", "User")};
            }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<ApplicationUser> returnVal = model.Set<ApplicationUser>()
                .AsNoTracking();
            var a = (Expression<Func<ApplicationUser, bool>>)GetParentWhere(Query, QueryType);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<ApplicationUser, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public bool ImportRecords => false;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool SetAvtive => false;
        public bool CanSetActive => false;// => false;
    }
}
