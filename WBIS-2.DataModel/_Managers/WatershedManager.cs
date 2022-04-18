using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class WatershedManager : IInfoTypeManager
    {
        public string DisplayName { get { return "Watershed"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new Hex160(), new CNDDBOccurrence(), new CDFW_SpottedOwl(), new SiteCalling(), new OwlBanding(), new SPIPlantPolygon(), 
                new SPIPlantPoint(), new AmphibianSurvey(), new AmphibianElement(), new BotanicalScoping(), new BotanicalSurveyArea(), new BotanicalSurvey(), new BotanicalElement() }; }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<Watershed> returnVal = model.Set<Watershed>();
            var a = (Expression<Func<Watershed, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                returnVal = returnVal.Include(_ => _.Districts).Where(a);
            else if (QueryType == typeof(Quad75))
                returnVal = returnVal.Include(_ => _.Quad75s).Where(a);
            else if (QueryType == typeof(Hex160))
                returnVal = returnVal.Include(_ => _.Hex160s).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Watershed, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
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
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("WatershedName", "Watershed"),
                new KeyValuePair<string, string>("WatershedID", "Watershed")};
            }
        }
        public bool CanSetActive => false;// => false;
    }
}
