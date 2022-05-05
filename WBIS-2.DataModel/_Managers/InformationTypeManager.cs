﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WBIS_2.DataModel
{
    public class InformationTypeManager<InfoType> : IInfoTypeManager where InfoType : class
    {
        public string DisplayName => GetDisplayName();
        public Type InformationType => typeof(InfoType);
        private string GetDisplayName()
        {
            string initial = typeof(InfoType).Name;
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


        private IEnumerable<PropertyInfo> ListInfoProperties => typeof(InfoType).GetProperties().Where(_ => _.GetCustomAttribute(typeof(ListInfo)) != null);
        private IEnumerable<PropertyInfo> AutoIncludes => ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).AutoInclude);
        public IEnumerable<PropertyInfo> DisplayFieldProperties => ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).DisplayField);
        public IInformationType[] AvailibleChildren => GetAvailibleChildren();

        private IInformationType[] GetAvailibleChildren()
        {
            List<IInformationType> children = new List<IInformationType>();

            var properites = ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).ChildField);
            foreach (var item in properites)
            {
                var runTimeType = item.PropertyType.GenericTypeArguments.Single();
                var trueType = Type.GetType(runTimeType.FullName);
                children.Add((IInformationType)Activator.CreateInstance(trueType));
            }
            return children.ToArray();
        }

        /// <summary>
        /// For each of the auto included properties get the fields that should be displayed for them.
        /// </summary>
        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                List<KeyValuePair<string, string>> valuePairs = new List<KeyValuePair<string, string>>();

                foreach (var prop in AutoIncludes)
                {
                    valuePairs.AddRange(GetDisplayFields(prop.PropertyType, $"{prop.Name}."));
                }

                return valuePairs;
            }
        }

        private List<KeyValuePair<string, string>> GetDisplayFields(Type type, string classStack)
        {
            List<KeyValuePair<string, string>> valuePairs = new List<KeyValuePair<string, string>>();
            if (!IsIInformationType(type)) return valuePairs;

            var properties = ((IInformationType)Activator.CreateInstance(type)).Manager.DisplayFieldProperties;

            foreach (var item in properties)
            {                
                if (IsIInformationType(item.PropertyType))
                {
                    valuePairs.AddRange(GetDisplayFields(item.PropertyType, $"{classStack}{item.Name}."));
                }
                else
                {
                    valuePairs.Add(new KeyValuePair<string, string>(item.Name, $"{classStack}{item.Name}"));
                }
            }
            return valuePairs;
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<InfoType> returnVal = model.Set<InfoType>()
                .AsNoTracking();
            
            PropertyInfo queryProperty = ParentChildPropertyProperty(QueryType);

            foreach (var include in AutoIncludes)
                ThenIncludes(ref returnVal, include);

            if (queryProperty != null)
                if (!AutoIncludes.Contains(queryProperty))
                    returnVal = returnVal.Include($"{queryProperty.Name}");

            var info = this.GetType().GetMethod(nameof(GetParentWhere));
            var genInfo = info.MakeGenericMethod(QueryType);
            var a = (Expression<Func<InfoType, bool>>)genInfo
                .Invoke(this, new object[] { Query.ToList(), queryProperty });

            return  returnVal.Where(a).ToList().AsQueryable();
        }

        /// <summary>
        /// Auto includes may have further neccissary inclussion for display purposes
        /// </summary>
        private void ThenIncludes(ref IQueryable<InfoType> query, PropertyInfo includeProp)
        {
            bool furtherInclusions = false;
            if (IsIInformationType(includeProp.PropertyType))
            {
                var IncludedInstance = Activator.CreateInstance(includeProp.PropertyType);
                foreach (var potentialInclusion in ((IInformationType)IncludedInstance).Manager.DisplayFieldProperties)
                {
                    if (IsIInformationType(potentialInclusion.PropertyType))
                    {
                        furtherInclusions = true;
                        query = query.Include($"{includeProp.Name}.{potentialInclusion.Name}");
                    }
                }
            }

            if (!furtherInclusions) 
                query = query.Include($"{includeProp.Name}");
        }



        public Expression GetParentWhere<z>(List<object> Query, PropertyInfo queryProperty) where z : class
        {
            Expression<Func<InfoType, bool>> a;
            var parameterExp = Expression.Parameter(typeof(InfoType), "type");
               

            if (queryProperty == null)
            {
                var queryCast = Query.Cast<InfoType>();
                //Expression<Func<InfoType, bool>> predicate = b => queryCast.Contains(b); //a = _ => Query.Contains(_);
               // a = Expression.Lambda<Func<InfoType, bool>>(predicate, parameterExp);
                a = _ => queryCast.Contains(_);
            }
            else if (!queryProperty.PropertyType.Name.Contains("ICollection"))
            {
                var propertyExp = Expression.Property(parameterExp, queryProperty);
                MethodInfo method = Query.GetType().GetMethod("Contains", new[] { typeof(z) });
                var someValue = Expression.Constant(Query);
                var containsMethodExp = Expression.Call(someValue, method, propertyExp);

                a = Expression.Lambda<Func<InfoType, bool>>(containsMethodExp, parameterExp);
            }
            else if (queryProperty.PropertyType.Name.Contains("ICollection"))
            {
                var propertyExp = Expression.Property(parameterExp, queryProperty);
                var queryCast = Query.Cast<z>();
                Expression<Func<z, bool>> predicate = b => queryCast.Contains(b);

                var body = Expression.Call(typeof(Enumerable), "Any", new[] { typeof(z) },
                    propertyExp, predicate);

                a = Expression.Lambda<Func<InfoType, bool>>(body, parameterExp);
            }
            else
            {
                throw new NotImplementedException();
            }
            return a;
        }

        public PropertyInfo ParentChildPropertyProperty(Type type)
        {
            var q = typeof(InfoType).GetProperties();
            PropertyInfo propertyInfo = typeof(InfoType).GetProperties().FirstOrDefault(_ => _.PropertyType == type);
            if (propertyInfo == null)
                propertyInfo = typeof(InfoType).GetProperties().FirstOrDefault(_ => _.PropertyType.FullName.Contains(type.Name) && _.PropertyType.FullName.Contains("ICollection"));
            return propertyInfo;
        }

        public bool ImportRecords => false;
        public bool AddRecord => true;
        public bool AddRequiresParent => true;
        public bool DetailView => true;
        public bool DeleteRecord => true;
        public bool RestoreRecord => true;
        public bool CanSetActive => false;// => false;


        public bool IsIInformationType(Type type)
        { return type.GetInterfaces().Contains(typeof(IInformationType)); }
       
        
        public SubstituteLayer SubstituteLayer => (SubstituteLayer)typeof(InfoType).GetCustomAttributes(typeof(SubstituteLayer), true).FirstOrDefault();
        public string GetLayerName()
        {
            Type type;
            if (SubstituteLayer == null)
            {
                type = typeof(InfoType);
               // var p = typeof(WBIS2Model).GetProperties().First(_ => _.PropertyType.GetGenericArguments().Single() == typeof(InfoType));
               // name = p.Name;
            }
            else
            {
                type = SubstituteLayer.SubLayer;
                //var p = typeof(WBIS2Model).GetProperties().First(_ => _.PropertyType.GetGenericArguments().Single() == SubstituteLayer.SubLayer);
                //name = p.Name;
            }

            WBIS2Model model = new WBIS2Model();
            var entityType = model.Model.FindEntityType(type);
            var schema = entityType.GetSchema();
            return entityType.GetTableName().ToLower();
        }
    }
}
