using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class SPIPlantPointManager : IInfoTypeManager
    {
        public string DisplayName { get { return "SPI Plant Point"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<SPIPlantPoint> returnVal = model.Set<SPIPlantPoint>();
            var a = (Expression<Func<SPIPlantPoint, bool>>)GetParentWhere(Query, QueryType);

                returnVal = returnVal.Include(_ => _.District)
                    .Include(_ => _.Watershed)
                    .Include(_ => _.Quad75)
                    .Include(_ => _.Hex160)
                    .Include(_=>_.PlantSpecies).Where(a);
            return CanSetActive ? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<SPIPlantPoint, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => Query.Cast<Watershed>().Contains(_.Watershed);
            else if (QueryType == typeof(Quad75))
                a = _ => Query.Cast<Quad75>().Contains(_.Quad75);
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
        public bool CanSetActive => false;
    }
}
