using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class THP_AreaManager : IInfoTypeManager
    {
        public string DisplayName { get { return "THP Area"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalScoping(), new BotanicalSurveyArea() }; }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<THP_Area> returnVal = model.Set<THP_Area>();
            var a = (Expression<Func<THP_Area, bool>>)GetParentWhere(Query, QueryType);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<THP_Area, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("THPName", "THP_Area")};
            }
        }
        public bool CanSetActive => false;
    }
}
