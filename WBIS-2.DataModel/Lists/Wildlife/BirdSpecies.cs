using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BirdSpecies : IInformationType
    {
        [Key,Column("guid")]
        public Guid Guid { get; set; }
        [Column("species")]
        public string Species { get; set; }

        [Column("is_surveyable")]
        public bool IsSurveyable { get; set; }
        [Column("is_findable")]
        public bool IsFindable { get; set; }
        [Column("banding_species")]
        public bool BandingSpecies { get; set; }


        [InverseProperty("SurveySpecies")]
        public ICollection<SiteCalling> SurveySpecies { get; set; }
        [InverseProperty("SpeciesFound")]
        public ICollection<SiteCallingDetection> SpeciesFound { get; set; }


        public ICollection<OwlBanding> OwlBandings { get; set; }
        public ICollection<Hex160RequiredPass> PassSpecies { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager<IInformationType> Manager => (IInfoTypeManager<IInformationType>)new BirdSpeciesManager();
    }

    public class BirdSpeciesManager : IInfoTypeManager<AmphibianSpecies>
    {
        public string DisplayName { get { return "Bird Species"; } }

        public IQueryable<AmphibianSpecies> GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<AmphibianSpecies>();
            var a = (Expression<Func<AmphibianSpecies, bool>>)GetParentWhere(Query, QueryType);

            return returnVal.Where(a);
        }
        public Expression<Func<AmphibianSpecies, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<AmphibianSpecies, bool>> a = _ => Query.Contains(_);
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
                { new KeyValuePair<string, string>("Species", "BirdSpecies")};
            }
        }
    }
}
