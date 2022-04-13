using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class WildlifeSpecies : IInformationType
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Column("alpha_code")]
        public string AlphaCode { get; set; }
        [Column("wildlife_species_description")]
        public string WildlifeSpeciesDescription { get; set; }
        [Column("class")]
        public string Class { get; set; }
        [Column("order")]
        public string Order { get; set; }
        [Column("family")]
        public string Family { get; set; }
        [Column("genus")]
        public string Genus { get; set; }
        [Column("species")]
        public string Species { get; set; }
        [Column("wl_sorting")]
        public int WLSorting { get; set; }
        [Column("sub_species")]
        public string SubSpecies { get; set; }
        [Column("whr_num")]
        public string WHRNum { get; set; }

        public ICollection<OtherWildlife> OtherWildlifeRecords { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new WildlifeSpeciesManager();
    }
    public class WildlifeSpeciesManager : IInfoTypeManager
    {
        public string DisplayName { get { return "Wildlife Species"; } }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<WildlifeSpecies>();
            var a = (Expression<Func<WildlifeSpecies, bool>>)GetParentWhere(Query, QueryType);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<WildlifeSpecies, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("AlphaCode", "WildlifeSpecies"),
                new KeyValuePair<string, string>("Species", "WildlifeSpecies")};
            }
        }
    }
}
