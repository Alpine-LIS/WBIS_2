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
    public class District : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("district_name")]
        public string DistrictName { get; set; }
        [Required, Column("management_area")]
        public string ManagementArea { get; set; }



        //[Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        //public MultiPolygon Geometry { get; set; }

        //[Column("district_extended_geometry_id")]
        //public Guid DistrictExtendedGeometryID { get; set; }
        public DistrictExtendedGeometry DistrictExtendedGeometry { get; set; }


        public ICollection<Hex160> Hex160s { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }
        public ICollection<CDFW_SpottedOwlDiagram> CDFW_SpottedOwlDiagrams { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<OwlBanding> OwlBandings { get; set; }
        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get; set; }
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        public ICollection<AmphibianElement> AmphibianElements { get; set; }

        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        public ICollection<BotanicalElement> BotanicalElements { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager<IInformationType> Manager => (IInfoTypeManager<IInformationType>)new DistrictManager();
    }

    public class DistrictManager : IInfoTypeManager<District>
    {
        public string DisplayName { get { return "District"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new Quad75(),new Watershed(), new Hex160(), new CNDDBOccurrence(), new CDFW_SpottedOwl(), new SiteCalling(), new OwlBanding(), new SPIPlantPolygon(),
                new SPIPlantPoint(), new AmphibianSurvey(), new AmphibianElement(), new BotanicalScoping(), new BotanicalSurveyArea(), new BotanicalSurvey(), new BotanicalElement() }; }
        }


        public IQueryable<District> GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<District>();
            var a = GetParentWhere(Query, QueryType);

            return returnVal.Where(a);
        }
        public Expression<Func<District, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<District, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("DistrictName", "District"),
                new KeyValuePair<string, string>("ManagementArea", "District")};
            }
        }
    }
}
