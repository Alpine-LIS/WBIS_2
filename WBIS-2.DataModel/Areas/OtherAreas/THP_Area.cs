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
    public class THP_Area : IInformationType, IQueryStuff<THP_Area>
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
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalScoping(), new BotanicalSurveyArea() }; }
        }
        public Expression<Func<THP_Area, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<THP_Area, bool>> a;
            if (QueryType == typeof(BotanicalScoping))
                a = _ => _.BotanicalScopins.Any(d => Query.Cast<BotanicalScoping>().Contains(d));
            else if (QueryType == typeof(BotanicalSurveyArea))
                a = _ => _.BotanicalSurveyAreas.Any(d => Query.Cast<BotanicalSurveyArea>().Contains(d));            
            else
                a = _ => Query.Contains(_);
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
