using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WBIS_2.DataModel
{
    public class Hex160Manager : IInfoTypeManager
    { 
        public string DisplayName { get { return "Hex160"; } }

        public IInformationType[] AvailibleChildren
        {
            get
            {
                return new IInformationType[] { new ProtectionZone(), new PermanentCallStation(), new Hex160RequiredPass(), new SiteCalling(), new OwlBanding(),
                new AmphibianSurvey(), new AmphibianElement(), new SPIPlantPolygon(), new SPIPlantPoint(),
            new CNDDBOccurrence(),new CDFW_SpottedOwl(),new BotanicalSurveyArea(),new BotanicalSurvey(),new BotanicalElement()};
            }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<Hex160> returnVal = model.Set<Hex160>()
                .AsNoTracking()
                .Include(_=>_.CurrentProtectionZone);
            var a = (Expression<Func<Hex160, bool>>)GetParentWhere(Query, QueryType);

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
            Expression<Func<Hex160, bool>> a;
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
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("Hex160ID", "Hex160")};
            }
        }

        public bool CanSetActive => true;
    }
}
