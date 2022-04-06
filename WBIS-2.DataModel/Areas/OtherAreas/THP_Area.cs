using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class THP_Area : IInformationType, IQueryStuff, IParentChild//<THP_Area>
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("thp_name")]
        public string THPName { get; set; }


        public ICollection<BotanicalScoping> BotanicalScopins { get; set; }
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "THP Area"; } }
        [NotMapped]
        public ICollection<IChild> Children
        {
            get
            {
                return ParentChildQuerries.GetChildren(this.GetType());
            }
        }
        [NotMapped]
        public ICollection<IParent> Parents
        {
            get
            {
                return ParentChildQuerries.GetParents(this.GetType());
            }
        }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalScoping(), new BotanicalSurveyArea() }; }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<THP_Area>();
            var a = (Expression<Func<THP_Area, bool>>)GetParentWhere(Query, QueryType);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<THP_Area, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("THPName", "THP_Area")};
            }
        }
    }
}
