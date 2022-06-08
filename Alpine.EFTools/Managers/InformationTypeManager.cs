using Alpine.EFTools.Attributes;
using Alpine.EFTools.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Alpine.EFTools.Managers
{
    public class InformationTypeManager<InfoType> : IInfoTypeManager where InfoType : class
    {
        public string DisplayName => GetDisplayName();
        public string GetTableName(DbContext context)
        {
            var entityType = context.Model.FindEntityType(InformationType);
            var schema = entityType.GetSchema();
            return entityType.GetTableName().ToLower();
        }

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

                //if ((trueType.GetInterfaces().Contains(typeof(IWildlifeRecord)) && CurrentUser.User.Wildlife) || (trueType.GetInterfaces().Contains(typeof(IBotanyRecord)) && CurrentUser.User.Botany))
                    children.Add((IInformationType)Activator.CreateInstance(trueType));
            }
            return children.OrderBy(_ => ((DisplayOrder)_.GetType().GetCustomAttribute(typeof(DisplayOrder))).Index).ToArray();
        }



        public IInformationType[] PossibleParents => GetPossibleParents();
        private IInformationType[] GetPossibleParents()
        {
            List<IInformationType> parents = new List<IInformationType>();

            var properites = ListInfoProperties.Where(_ => ((ListInfo)_.GetCustomAttribute(typeof(ListInfo))).ParentField);
            foreach (var item in properites)
            {
                parents.Add((IInformationType)Activator.CreateInstance(item.PropertyType));
            }
            return parents.ToArray();
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




        public string GetSqlQuery(Dictionary<string, string> replace, DbContext context)
        {
            var query = "Select ";
            var entityType = typeof(InfoType);
            //using var context = new WBIS2Model();

            if (context.Model.FindEntityType(typeof(InfoType)) is IEntityType et)
            {
                var properties = et.GetProperties();
                //TODO:
                /* 
                var navs = et.GetIndexes();
                var other = et.GetNavigations();
                var fc = et.GetReferencingForeignKeys();
                */
                foreach (var prop in properties)
                {
                    string dbName = prop.Name;
                    PropertyInfo propertyInfo = typeof(InfoType).GetProperty(prop.Name);
                    var sub = propertyInfo.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), true).FirstOrDefault();                   
                   
                    if (sub != null)
                        dbName = ((System.ComponentModel.DataAnnotations.Schema.ColumnAttribute)sub).Name;

                    if (replace.Keys.Contains(prop.Name)) 
                        query += $"{replace[prop.Name]} as \"{dbName}\" ,";
                    else if (replace.Keys.Contains(dbName)) 
                        query += $"{replace[dbName]} as \"{dbName}\" ,";
                    else
                        query += $"\"{dbName}\" ,";
                }
                query = query.Remove(query.Length - 1, 1);

                //Guid guid = CurrentUser.User.Guid;
                //if (CurrentUser.MobileUserActiveUnits)
                //    guid = CurrentUser.MobileUserGuid;

                //if (CanSetActive) query = query.Replace($"\"is_active\"", $"guid IN (SELECT unit_id FROM active_{et.GetSchemaQualifiedTableName()} WHERE application_user_id = '{guid}') as \"is_active\"");

                query += $" from \"{et.GetSchemaQualifiedTableName()}\"";
            }
            return query;
        }


        public IQueryable GetQueryable(DbContext model, bool track = false, List<string> ForceInclude = null, bool includeGeometry = false)
        {
            IQueryable<InfoType> returnVal;

            Dictionary<string, string> QueryExclude = new Dictionary<string, string>();
            if (!includeGeometry)
                QueryExclude.Add("geometry","null");

            if (track) 
                returnVal= model.Set<InfoType>()
                    .FromSqlRaw(GetSqlQuery(QueryExclude, model));
            else
                returnVal = model.Set<InfoType>()
                   .FromSqlRaw(GetSqlQuery(QueryExclude, model))
                   .AsNoTracking();

            foreach (var include in AutoIncludes)
                ThenIncludes(ref returnVal, include);

            if (ForceInclude != null)
                foreach (var include in ForceInclude)
                    returnVal = returnVal.Include(include);
                     
            return returnVal;
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, DbContext model, bool showDelete = false, bool showRepository = false, 
            bool includeGeometry = false, bool track = false, List<string> ForceInclude = null)
        {
            IQueryable<InfoType> returnVal = (IQueryable<InfoType>)GetQueryable(model, track, ForceInclude, includeGeometry);

            if (Query.Length == 0)
                return returnVal.Take(0);

            PropertyInfo queryProperty = GetQueryProperty(returnVal,QueryType);

            var ShowOptions = (Expression<Func<InfoType, bool>>)ShowHideDeleteAndRepository(showDelete, showRepository);

            if (queryProperty == null && typeof(InfoType) != QueryType)
                //if ((queryProperty == null && typeof(InfoType) != QueryType) || typeof(InfoType) == typeof(ApplicationUser))
            {
                if (ShowOptions == null)
                    return returnVal;
                else
                    return returnVal.Where(ShowOptions);
            }
            else
            {
                var info = this.GetType().GetMethod(nameof(GetParentWhere));
                var genInfo = info.MakeGenericMethod(QueryType);
                var ParentQuery = (Expression<Func<InfoType, bool>>)genInfo
                    .Invoke(this, new object[] { Query.ToList(), queryProperty });


                if (ShowOptions == null)
                    return returnVal.Where(ParentQuery);
                else
                    return returnVal.Where(ParentQuery).Where(ShowOptions);
            }           
        }

        private PropertyInfo GetQueryProperty(IQueryable<InfoType> returnVal, Type QueryType)
        {
            PropertyInfo queryProperty = null;
            if (QueryType != typeof(InfoType))
            {
                queryProperty = ParentChildPropertyProperty(QueryType);
                if (queryProperty != null)
                    if (!AutoIncludes.Contains(queryProperty))
                        returnVal = returnVal.Include($"{queryProperty.Name}");
            }
            return queryProperty;
        }

        //public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        //{
        //    return GetQueryable(Query, QueryType, model, true, true, false, null);
        //}
        //public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool showDelete, bool showRepository)
        //{
        //    return GetQueryable(Query, QueryType, model, showDelete, showRepository, false, null);
        //}
        //public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool showDelete, bool showRepository, bool includeGeometry)
        //{
        //    return GetQueryable(Query, QueryType, model, showDelete, showRepository, false, null, includeGeometry);
        //}
        //public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, List<string> ForceInclude)
        //{
        //    return GetQueryable(Query, QueryType, model, true, true, false, ForceInclude);
        //}
        //public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model, bool track)
        //{
        //    return GetQueryable(Query, QueryType, model, true, true, track, null);
        //}

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

        public Expression ShowHideDeleteAndRepository(bool showDelete, bool showRepository)
        {
            Expression<Func<InfoType, bool>> a;
            var parameterExp = Expression.Parameter(typeof(InfoType), "type");
            PropertyInfo propDelete = typeof(InfoType).GetProperties().FirstOrDefault(_ => _.Name == "_delete");
            Expression showDeleteExpression = null;
            if (propDelete != null && !showDelete)
            {
                var propertyExp = Expression.Property(parameterExp, propDelete);
                showDeleteExpression = Expression.Equal(propertyExp, Expression.Constant(false, typeof(bool)));
            }


            PropertyInfo propRepo = typeof(InfoType).GetProperties().FirstOrDefault(_ => _.Name == "Repository");
            Expression showRepoExpression = null;
            if (propRepo != null && !showRepository)
            {
                var propertyExp = Expression.Property(parameterExp, propRepo);
                showRepoExpression = Expression.Equal(propertyExp, Expression.Constant(false, typeof(bool)));
            }

            if (showRepoExpression != null && showDeleteExpression != null)
                return a = Expression.Lambda<Func<InfoType, bool>>(Expression.And(showDeleteExpression, showRepoExpression), parameterExp);
            else if (showRepoExpression != null)
                return a = Expression.Lambda<Func<InfoType, bool>>(showRepoExpression, parameterExp);
            else if (showDeleteExpression != null)
                return a = Expression.Lambda<Func<InfoType, bool>>(showDeleteExpression, parameterExp);
            else return null;
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





        public IQueryable GetQueryableFromChildren(object[] Query, Type QueryType, DbContext model)
        {
            IQueryable<InfoType> returnVal = (IQueryable<InfoType>)GetQueryable(model, true, null);

            PropertyInfo queryProperty = null;
            if (QueryType != typeof(InfoType))
            {
                queryProperty = ParentChildPropertyProperty(QueryType);
                if (queryProperty != null)
                    if (!AutoIncludes.Contains(queryProperty))
                        returnVal = returnVal.Include($"{queryProperty.Name}");
            }

            var info = this.GetType().GetMethod(nameof(GetParentFromChild));
            var genInfo = info.MakeGenericMethod(QueryType);
            var a = (Expression<Func<InfoType, bool>>)genInfo
                .Invoke(this, new object[] { Query.ToList(), queryProperty });

            return returnVal.Where(a);
        }

        public Expression GetParentFromChild<z>(List<object> Query, PropertyInfo queryProperty) where z : class
        {
            Expression<Func<InfoType, bool>> a;
            var parameterExp = Expression.Parameter(typeof(InfoType), "type");

            var propertyExp = Expression.Property(parameterExp, queryProperty);
            var queryCast = Query.Cast<z>();
            Expression<Func<z, bool>> predicate = b => queryCast.Contains(b);

            var body = Expression.Call(typeof(Enumerable), "Any", new[] { typeof(z) },
                propertyExp, predicate);

            return a = Expression.Lambda<Func<InfoType, bool>>(body, parameterExp);
        }





        public bool ImportRecords => false;
        public bool AddRequiresParent => false;
        public bool DeleteRestoreRecord => typeof(InfoType).GetProperties().Any(_=>_.Name == "_delete");
        public bool RepositoryRecord => typeof(InfoType).GetProperties().Any(_ => _.Name == "Repository");
        //public bool CanSetActive => typeof(InfoType).GetInterfaces().Contains(typeof(IActiveUnit));
        //public bool CanSetActive => typeof(InfoType).GetInterfaces().Contains(typeof(IActiveUnit));


        public bool IsIInformationType(Type type)
        { return type.GetInterfaces().Contains(typeof(IInformationType)); }
       
        
        public SubstituteLayer SubstituteLayer => (SubstituteLayer)typeof(InfoType).GetCustomAttributes(typeof(SubstituteLayer), true).FirstOrDefault();
        public string GetLayerName(DbContext context)
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

            //WBIS2Model model = new WBIS2Model();
            //var entityType = model.Model.FindEntityType(type);
            var entityType = context.Model.FindEntityType(type);
            var schema = entityType.GetSchema();
            return entityType.GetTableName().ToLower();
        }       
    }
}
