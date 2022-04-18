using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WBIS_2.DataModel
{
    public class Quad75Manager:IInfoTypeManager
    {
        public string DisplayName { get { return "Quad75"; } }
        public IInformationType[] AvailibleChildren
        {
            get
            {
                return new IInformationType[] { new Hex160(), new CNDDBOccurrence(), new CDFW_SpottedOwl(), new SiteCalling(), new OwlBanding(), new SPIPlantPolygon(),
                new SPIPlantPoint(), new AmphibianSurvey(), new AmphibianElement(), new BotanicalScoping(), new BotanicalSurveyArea(), new BotanicalSurvey(), new BotanicalElement()
            };
            }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<Quad75> returnVal = model.Set<Quad75>();
            var a = (Expression<Func<Quad75, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                returnVal = returnVal.Include(_ => _.Districts).Where(a);
            else if (QueryType == typeof(Watershed))
                returnVal = returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Hex160))
                returnVal = returnVal.Include(_ => _.Hex160s).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }

        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Quad75, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
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
                { new KeyValuePair<string, string>("QuadCode", "Quad75")};
            }
        }
        public bool CanSetActive => false;// => false;
    }
}
