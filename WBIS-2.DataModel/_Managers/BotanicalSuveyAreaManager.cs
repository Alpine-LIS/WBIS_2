﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class BotanicalSuveyAreaManager : IInfoTypeManager, IRecordOptions
    {
        public string DisplayName { get { return "Botanical Survey Area"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalSurvey(), new BotanicalElement() }; }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<BotanicalSurveyArea> returnVal = model.Set<BotanicalSurveyArea>()
                .Include(_=>_.District)
                .Include(_=>_.THP_Area)
                .Include(_=>_.User)
                .Include(_=>_.UserModified);
            var a = (Expression<Func<BotanicalSurveyArea, bool>>)GetParentWhere(Query, QueryType);

             if (QueryType == typeof(Watershed))
                returnVal = returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                returnVal = returnVal.Include(_ => _.Quad75s).Where(a);
            else if (QueryType == typeof(Hex160))
                returnVal = returnVal.Include(_ => _.Hex160s).Where(a);
            else if (QueryType == typeof(BotanicalScoping))
                returnVal = returnVal.Include(_ => _.BotanicalScoping).Where(a);

            return CanSetActive? returnVal.Where(a).ToList().AsQueryable() : returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<BotanicalSurveyArea, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            else if (QueryType == typeof(BotanicalScoping))
                a = _ => Query.Cast<BotanicalScoping>().Contains(_.BotanicalScoping);
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

        public bool ImportRecords => true;

        public bool AddRecord => true;

        public bool AddRequiresParent => false;

        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
           public bool CanSetActive => false;// => true;
    }
}