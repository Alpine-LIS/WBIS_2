using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WBIS_2.DataModel
{
    public class InformationTypeManager<t> : IInfoTypeManager, IRecordOptions where t : class
    {
        public string DisplayName => GetDisplayName();
        private string GetDisplayName()
        {
            string initial = typeof(t).Name;
            string returnVal = initial.First().ToString();

            for (int i = 1; i < initial.Length - 1; i++)
            {
                if (!Char.IsLetterOrDigit(initial[i]))
                    returnVal += " ";
                else if (Char.IsUpper(initial[i]) && Char.IsLower(initial[i + 1]))
                    returnVal += " " + initial[i];
                else returnVal += initial[i];
            }
            return returnVal + initial.Last();
        }

        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public IEnumerable<PropertyInfo> ListInfoProperties => typeof(t).GetProperties().Where(_ => _.GetCustomAttribute(typeof(ListInfo)) != null);
        public IEnumerable<PropertyInfo> AutoIncludes => ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).AutoInclude);
        //public IEnumerable<PropertyInfo> DisplayFields => null;


        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<t> returnVal = model.Set<t>()
                .AsNoTracking();

            foreach (var include in AutoIncludes)
                returnVal = returnVal.Include($"{include.Name}");

            PropertyInfo queryProperty = ParentChildPropertyProperty(QueryType);
            if (!AutoIncludes.Contains(queryProperty))
                returnVal = returnVal.Include($"{queryProperty.Name}");


            //foreach (var item in Inclusions)
            //{
            //    returnVal.Include(_ => _.GetType().GetProperty(item.Name));
            //}

            var a = (Expression<Func<t, bool>>)GetParentWhere(Query, QueryType);

            //if (QueryType == typeof(Watershed) qType)
            //    returnVal = returnVal.Include(_ => _.qType.).Where(a);
            //else if (QueryType == typeof(Quad75))
            //    returnVal = returnVal.Include(_ => _.Quad75).Where(a);

            return  returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            //https://stackoverflow.com/questions/8625/generic-type-conversion-from-string

            Expression<Func<t, bool>> a;

            ICollection<t> yes;

            PropertyInfo queryProperty = ParentChildPropertyProperty(QueryType);
            if (!queryProperty.Name.Contains("ICollection"))
                a = _ => Query.Contains(queryProperty.GetValue(_));
            else if (queryProperty.Name.Contains("ICollection"))
            {
                a = _ => ((dynamic)queryProperty.GetValue(_)).Any(a => Query.Contains(queryProperty.GetValue(a)));
            }
                
            else
                //if (QueryType == typeof(District))
                //    a = _ => _.Hex160.Districts.Any(d => Query.Cast<District>().Contains(d));
                //else if (QueryType == typeof(Watershed))
                //    a = _ => _.Hex160.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
                //else if (QueryType == typeof(Quad75))
                //    a = _ => _.Hex160.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
                //else if (QueryType == typeof(Hex160))
                //    a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
                //else
                a = _ => Query.Contains(_);
            return a;
        }

        public PropertyInfo ParentChildPropertyProperty(Type type)
        {
            PropertyInfo propertyInfo = typeof(t).GetProperties().FirstOrDefault(_ => _.PropertyType == type);
            if (propertyInfo == null)
                propertyInfo = typeof(t).GetProperties().First(_ => _.PropertyType.FullName.Contains(type.Name) && _.PropertyType.Name.Contains("ICollection"));
            return propertyInfo;
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

        public bool AddRequiresParent => true;

        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;// => false;
    }
}
