using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class OwlBandingManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Owl Banding"; } }

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
                return new List<KeyValuePair<string, string>>();
            }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<OwlBanding> returnVal = model.Set<OwlBanding>()
                .AsNoTracking()
                .Include(_ => _.User)
                .Include(_ => _.UserModified)
                .Include(_ => _.BirdSpecies)
                .Include(_=>_.ProtectionZone)
                .Include(_=>_.District)
                .Include(_=>_.Hex160);
            var a = (Expression<Func<OwlBanding, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(Watershed))
                returnVal = returnVal.Include(_ => _.Watershed).Where(a);
            else if (QueryType == typeof(Quad75))
                returnVal = returnVal.Include(_ => _.Quad75).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<OwlBanding, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => Query.Cast<Watershed>().Contains(_.Watershed);
            else if (QueryType == typeof(Quad75))
                a = _ => Query.Cast<Quad75>().Contains(_.Quad75);
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            a = _ => Query.Contains(_);
            return a;
        }

        public bool ImportRecords => false;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;
    }
}
