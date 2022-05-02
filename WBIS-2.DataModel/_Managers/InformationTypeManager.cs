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
    public class InformationTypeManager { }
    public class InformationTypeManager<InfoType> : IInfoTypeManager, IRecordOptions where InfoType : class
    {
        public string DisplayName => GetDisplayName();
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


        public IEnumerable<PropertyInfo> ListInfoProperties => typeof(InfoType).GetProperties().Where(_ => _.GetCustomAttribute(typeof(ListInfo)) != null);
        public IEnumerable<PropertyInfo> AutoIncludes => ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).AutoInclude);
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
        List<KeyValuePair<string, string>> IListManager.DisplayFields => throw new NotImplementedException();

        private List<KeyValuePair<string, string>> GetDisplayFields()
        {
            List<KeyValuePair<string, string>> valuePairs = new List<KeyValuePair<string, string>>();

            var properites = ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).DisplayField);
            foreach (var item in properites)
            {
                var runTimeType = item.PropertyType.GenericTypeArguments.Single();
                var trueType = Type.GetType(runTimeType.FullName);

                if (trueType.GetInterfaces().Contains(typeof(IInformationType)))
                {
                    valuePairs.AddRange(GetDisplayFields(trueType));
                }
                else
                {
                    valuePairs.Add(new KeyValuePair<string, string>(item.Name, trueType.Name));
                }

                var properites2 = typeof(InfoType).GetProperties().Where(_ => _.GetCustomAttribute(typeof(ListInfo)) != null)
                    .Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).DisplayField);
                foreach(var item2 in properites2)
                {

                    valuePairs.Add(GetDisplayValuePair(item2.PropertyType, "")); 

                    if (item2.PropertyType.GetInterfaces().Contains(typeof(IInformationType)))
                    {

                    }
                    else
                        valuePairs.Add(new KeyValuePair<string, string>(item.Name, trueType.Name));
                }


                if (prop.PropertyType.GetInterfaces().Contains(typeof(IInformationType)))
                {
                    var subProps = prop.PropertyType.GetProperties();
                    foreach (var subProp in subProps)
                    {
                        var subAtt = subProp.GetCustomAttributes(true).FirstOrDefault(_ => _.GetType() == typeof(ImportAttribute));
                        if (subAtt == null) continue;

                        string typeName = GetDataTypeString(subProp.PropertyType);
                        properties.Add(new PropertyType() { PropertyName = $"{prop.PropertyType.Name}.{subProp.Name}", TypeName = typeName, Required = ((ImportAttribute)subAtt).Required });
                    }
                }
                else
                {
                    string typeName = GetDataTypeString(prop.PropertyType);
                    properties.Add(new PropertyType() { PropertyName = prop.Name, TypeName = typeName, Required = ((ImportAttribute)att).Required });
                }

                { new KeyValuePair<string, string>("Species", "BirdSpecies")};

                valuePairs.Add(new KeyValuePair<string, string>(item.Name, trueType.Name));
            }
            return valuePairs;
        }

        private List<KeyValuePair<string, string>> GetDisplayFields(Type type, string classStack)
        {
            List<KeyValuePair<string, string>> valuePairs = new List<KeyValuePair<string, string>>();
            var properties = type.GetProperties().Where(_ => _.GetCustomAttribute(typeof(ListInfo)) != null)
                    .Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).DisplayField);

            foreach (var item in properties)
            {
                var runTimeType = item.PropertyType.GenericTypeArguments.Single();
                var trueType = Type.GetType(runTimeType.FullName);

                if (trueType.GetInterfaces().Contains(typeof(IInformationType)))
                {
                    valuePairs.AddRange(GetDisplayFields(trueType, $"{classStack}{trueType.Name}."));
                }
                else
                {
                    valuePairs.Add(new KeyValuePair<string, string>(item.Name, $"{classStack}{trueType.Name}"));
                }
            }

            return valuePairs;
        }

        private KeyValuePair<string, string> GetDisplayValuePair(Type propType, string classStack)
        {

        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            IQueryable<InfoType> returnVal = model.Set<InfoType>()
                .AsNoTracking();
            
            PropertyInfo queryProperty = ParentChildPropertyProperty(QueryType);

            foreach (var include in AutoIncludes)
                returnVal = returnVal.Include($"{include.Name}");

            if (queryProperty != null)
                if (!AutoIncludes.Contains(queryProperty))
                    returnVal = returnVal.Include($"{queryProperty.Name}");

            var info = this.GetType().GetMethod(nameof(GetParentWhere));
            var genInfo = info.MakeGenericMethod(QueryType);
            var a = (Expression<Func<InfoType, bool>>)genInfo
                .Invoke(this, new object[] { Query.ToList(), queryProperty });

            return  returnVal.Where(a).ToList().AsQueryable();
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

    }
}
