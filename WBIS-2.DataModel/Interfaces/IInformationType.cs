using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IInformationType : IParentChild
    {
        /// <summary>
        /// The user who created the record
        /// </summary>
        [NotMapped]
        public string DisplayName { get; }
        //public IInformationType[] AvailibleChildren { get; }
        /// <summary>
        /// Fields to be displayed when in an alternate information type.
        /// </summary>
        public List<KeyValuePair<string, string>> DisplayFields { get; }
    }
    //public interface IQueryStuff<T> where T : class
    //{
    //    public abstract Expression<Func<T, bool>> GetParentWhere(object[] Query, Type QueryType);
    //}
    public interface IQueryStuff
    {
        public abstract IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model);
        public abstract Expression GetParentWhere(object[] Query, Type QueryType);
    }

    public interface IParentChild
    {
        public ICollection<IChild> Children { get; }
        public ICollection<IParent> Parents { get; }
    }
    public interface IParent
    {
        public ICollection<IChild> Children { get; }
    }
    public interface IChild
    {
        public ICollection<IParent> Parents { get; }
    }

    public static class ParentChildQuerries
    {
        public static ICollection<IChild> GetChildren(Type infoType)
        {
            WBIS2Model context = new WBIS2Model();
            var a = context.Model.GetEntityTypes();//.FindEntityTypes(typeof(IParentChild));

            a.First(_=>_.)

            var b = a.Where(_ => IsIParentChild(_)).Select(_=>_.ConstructorBinding.RuntimeType);
            var properties = infoType.GetProperties();
            var c = new List<IChild>();
            foreach(var pC in b)
            {
                //Type specificListType = typeof(ICollection<>).MakeGenericType(pC.GetType());
                var col = Activator.CreateInstance(typeof(List<>).MakeGenericType(pC.GetType()));
                var collections = properties.Where(_ => typeof(ICollection<>).IsAssignableFrom(_.PropertyType));
                if (collections != null)
                {
                    if (collections.Any(_ => (_ as ICollection).AsQueryable().ElementType == pC.GetType()))
                        c.Add((IChild)pC);
                }

                if (properties.Any(_ => typeof(ICollection<>).IsAssignableFrom(_.PropertyType)))
                    c.Add((IChild)pC);
            }

            //s = s.Where(_ => (_ as IChild).Parents.Contains((IParent)inoType));
            //Type specificListType = typeof(ICollection<>).MakeGenericType(this.GetType());
            //s = s.Where(_ => _.GetType().GetProperties().FirstOrDefault(_ => _.GetType() == this.GetType() || _.GetType() == specificListType) != null);
            return c;// (ICollection<IChild>)s;
        }
        public static ICollection<IParent> GetParents(Type infoType)
        {
            WBIS2Model context = new WBIS2Model();
            var s = context.Model.FindEntityTypes(typeof(IParent));
            s = s.Where(_ => (_ as IParent).Children.Contains((IChild)infoType));
            //s = s.Where(_ => _.GetType().GetProperties().FirstOrDefault(_ => _.GetType() == typeof(ICollection<Hex160>)) != null);
            return (ICollection<IParent>)s;
        }
        public static bool IsIParentChild(IEntityType type)
        {
            return typeof(IParentChild).IsAssignableFrom(type.ConstructorBinding.RuntimeType);
        }

        public static Type GetListType<T>(this List<T> _)
        {
            return typeof(T);
        }
    }
}
