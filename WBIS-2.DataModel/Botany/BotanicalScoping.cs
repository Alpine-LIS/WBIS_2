using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalScoping : UserDataValidator, IUserRecords, IQueryStuff
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


      

        public ICollection<BotanicalElement> BotanicalElements { get; set; }
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        public ICollection<BotanicalScopingSpecies> BotanicalScopingSpecies { get; set; }


        [Column("thp_area_id")]
        public Guid THP_AreaId { get; set; }
        public THP_Area THP_Area { get; set; }

        
        public string Forester { get; set; }

        [Column("region_id")]
        public Guid RegionId { get; set; }
        public Region Region { get; set; }

        [Column("ecological_unit")]
        public string EcologicalUnit { get; set; }
        [Column("elevation_max")]
        public int ElevationMax { get; set; }
        [Column("elevation_min")]
        public int ElevationMin { get; set; }
        [Column("wshd_elevation_max")]
        public int WshdElevationMax { get; set; }
        [Column("wshd_elevation_min")]
        public int WshdElevationMin { get; set; }




        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }



        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }





        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }





        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Botanical Scoping"; } }
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
            { return new IInformationType[] { new BotanicalSurveyArea(), new BotanicalSurvey(), new BotanicalElement() }; }
        }



        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<BotanicalScoping>();
            var a = (Expression<Func<BotanicalScoping, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                return returnVal.Include(_ => _.Districts).Where(a);
            else if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                return returnVal.Include(_ => _.Quad75s).Where(a);

            return returnVal.Where(a);
        }

        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<BotanicalScoping, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
