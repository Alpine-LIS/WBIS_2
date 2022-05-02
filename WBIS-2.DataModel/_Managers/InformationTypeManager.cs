using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

            var info = this.GetType().GetMethod(nameof(GetParentWhere2));
            var genInfo = info.MakeGenericMethod(QueryType);
            var a = (Expression<Func<t, bool>>)genInfo.Invoke(this, new object[] { Query.ToList() });

            return  returnVal.Where(a).ToList().AsQueryable();
        }



        public Expression GetParentWhere2<z>(List<object> Query) where z : class
        {
            Expression<Func<t, bool>> a;
            PropertyInfo queryProperty = ParentChildPropertyProperty(typeof(z));
            var parameterExp = Expression.Parameter(typeof(t), "type");
            var propertyExp = Expression.Property(parameterExp, queryProperty);

            if (!queryProperty.PropertyType.Name.Contains("ICollection"))
            {
                MethodInfo method = Query.GetType().GetMethod("Contains", new[] { typeof(z) });
                var someValue = Expression.Constant(Query);
                var containsMethodExp = Expression.Call(someValue, method, propertyExp);

                a = Expression.Lambda<Func<t, bool>>(containsMethodExp, parameterExp);
            }
            else if (queryProperty.PropertyType.Name.Contains("ICollection"))
            {                                 
                var queryCast = Query.Cast<z>();
                Expression<Func<z, bool>> predicate = b => queryCast.Contains(b);

                var body = Expression.Call(typeof(Enumerable), "Any", new[] { typeof(z) },
                    propertyExp, predicate);

                a = Expression.Lambda<Func<t, bool>>(body, parameterExp);
            }
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public PropertyInfo ParentChildPropertyProperty(Type type)
        {
            var q = typeof(t).GetProperties();
            PropertyInfo propertyInfo = typeof(t).GetProperties().FirstOrDefault(_ => _.PropertyType == type);
            if (propertyInfo == null)
                propertyInfo = typeof(t).GetProperties().First(_ => _.PropertyType.FullName.Contains(type.Name) && _.PropertyType.FullName.Contains("ICollection"));
            return propertyInfo;
        }

        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            throw new NotImplementedException();
            List<object> result = new List<object>();
            result.Intersect(result).Any();
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
