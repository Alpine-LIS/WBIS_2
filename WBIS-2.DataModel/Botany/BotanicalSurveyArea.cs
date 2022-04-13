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
    public class BotanicalSurveyArea : UserDataValidator, IUserRecords, INonPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


      

        public ICollection<BotanicalElement> BotanicalElement { get; set; }
        public ICollection<BotanicalSurvey> BotanicalSurvey { get; set; }


        [Column("thp_area_id")]
        public Guid THP_AreaId { get; set; }
        public THP_Area THP_Area { get; set; }

        [Column("botanical_scoping_id")]
        public Guid BotanicalScopingId { get; set; }
        public BotanicalScoping BotanicalScoping { get; set; }


        [Column("area_name")]
        public string AreaName { get; set; }
        [Column("survey_type")]
        public string SurveyType { get; set; }
        [Column("general_habitat")]
        public string GeneralHabitat { get; set; }
        [Column("aspect")]
        public string Aspect { get; set; }
        [Column("slope")]
        public string Slope { get; set; }
        [Column("canopy")]
        public string Canopy { get; set; }
        [Column("rock_outcrops")]
        public string RockOutcrops { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
        [Column("boulders")]
        public string Boulders { get; set; }
        [Column("substrate")]
        public string Substrate { get; set; }
        [Column("talus_scree")]
        public bool TalusScree { get; set; } = false;
        [Column("lava_cap")]
        public bool LavaCap { get; set; } = false;
        [Column("spring_seep")]
        public bool SpringSeep { get; set; } = false;
        [Column("pond")]
        public bool Pond { get; set; } = false;
        [Column("other_wetlands")]
        public string OtherWetlands { get; set; }
        [Column("understory_vegetation")]
        public string UnderstoryVegetation { get; set; }
        [Column("surveys")]
        public int Surveys { get; set; }


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





        [Required, Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }


        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }





        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new BotanicalSuveyAreaManager(); } }
    }

    public class BotanicalSuveyAreaManager : IInfoTypeManager
    {
        public string DisplayName { get { return "Botanical Survey Area"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalSurvey(), new BotanicalElement() }; }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<BotanicalSurveyArea>()
                .Include(_=>_.District)
                .Include(_=>_.THP_Area)
                .Include(_=>_.User)
                .Include(_=>_.UserModified);
            var a = (Expression<Func<BotanicalSurveyArea, bool>>)GetParentWhere(Query, QueryType);

             if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                return returnVal.Include(_ => _.Quad75s).Where(a);
            else if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160s).Where(a);
            else if (QueryType == typeof(BotanicalScoping))
                return returnVal.Include(_ => _.BotanicalScoping).Where(a);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<BotanicalSurveyArea, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            else if (QueryType == typeof(BotanicalScoping))
                a = _ => Query.Cast<BotanicalScoping>().Contains(_.BotanicalScoping);
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
